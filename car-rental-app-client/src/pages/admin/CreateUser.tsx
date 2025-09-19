/*
// src/pages/admin/CreateUser.tsx
import { useForm } from "react-hook-form";
import { useState } from "react";
import axios, { type AxiosError } from "axios";
import {
    createUser,
    type CreateUserReq,
    type UserDto,
    type AddressDto,
} from "../../services/userService";

function sanitizeAddress(addr?: AddressDto): AddressDto | undefined {
    if (!addr) return undefined;

    const trimmed: AddressDto = {
        street: addr.street?.trim() ?? "",
        city: addr.city?.trim() ?? "",
        state: addr.state?.trim() ?? "",
        postalCode: addr.postalCode?.trim() ?? "",
        country: addr.country?.trim() ?? "",
    };

    // if all address fields are empty -> don't send address
    const allEmpty = Object.values(trimmed).every(v => !v);
    if (allEmpty) return undefined;

    // if any field provided, Street must be present (backend requires it)
    if (!trimmed.street) {
        throw new Error("Street is required when you provide any address field.");
    }

    return trimmed;
}

function extractApiError(e: AxiosError<any>): string {
    const data = e.response?.data;
    if (!data) return e.message;
    if (typeof data.detail === "string" && data.detail) return data.detail;

    if (data.errors && typeof data.errors === "object" && !Array.isArray(data.errors)) {
        const msgs: string[] = [];
        for (const [k, v] of Object.entries<any>(data.errors)) {
            const list = Array.isArray(v) ? v : [v];
            for (const m of list) msgs.push(`${k}: ${m}`);
        }
        if (msgs.length) return msgs.join(" | ");
    }

    if (Array.isArray(data.errors)) {
        const msgs = data.errors
            .map((x: any) => x?.errorMessage || x?.message || String(x))
            .filter(Boolean);
        if (msgs.length) return msgs.join(" | ");
    }

    if (data.title) return data.title;
    if (data.message) return data.message;
    return e.message;
}

export default function CreateUser() {
    // The form uses the same AddressDto shape as backend
    type FormModel = Omit<CreateUserReq, "address"> & { address?: AddressDto };

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<FormModel>({
        defaultValues: {
            role: "Customer",
            address: { street: "", city: "", state: "", postalCode: "", country: "" },
        },
    });

    const [loading, setLoading] = useState(false);
    const [created, setCreated] = useState<UserDto | null>(null);
    const [error, setError] = useState<string | null>(null);

    const onSubmit = async (data: FormModel) => {
        try {
            setLoading(true);
            setError(null);
            setCreated(null);

            const payload: CreateUserReq = {
                firstName: data.firstName.trim(),
                lastName: data.lastName.trim(),
                email: data.email.trim(),
                password: data.password,
                role: data.role,
                phone: data.phone?.trim() || undefined,
                username: data.username?.trim() || undefined,
                address: sanitizeAddress(data.address),
            };

            const user = await createUser(payload);
            setCreated(user);

            reset({
                firstName: "",
                lastName: "",
                email: "",
                password: "",
                role: "Customer",
                phone: "",
                username: "",
                address: { street: "", city: "", state: "", postalCode: "", country: "" },
            });
        } catch (e: unknown) {
            if (e instanceof Error && e.message.includes("Street is required")) {
                setError(e.message);
            } else if (axios.isAxiosError(e)) {
                setError(extractApiError(e));
            } else {
                setError("Unexpected error");
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={{ maxWidth: 720, margin: "24px auto" }}>
            {/!*<h2>Create User (Admin)</h2>*!/}
            <h2 className="text-2xl font-semibold mb-4 underline">Create User (Admin)</h2>


            <form onSubmit={handleSubmit(onSubmit)}>
                <fieldset style={{ border: "1px solid #ddd", padding: 12, marginBottom: 12 }}>
                    <legend>Basic</legend>

                    <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 12 }}>
                        <div>
                            <label>First Name</label><br />
                            <input {...register("firstName", { required: "First name is required" })} />
                            {errors.firstName && <small style={{ color: "red" }}>{errors.firstName.message}</small>}
                        </div>

                        <div>
                            <label>Last Name</label><br />
                            <input {...register("lastName", { required: "Last name is required" })} />
                            {errors.lastName && <small style={{ color: "red" }}>{errors.lastName.message}</small>}
                        </div>

                        <div>
                            <label>Email</label><br />
                            <input type="email" {...register("email", { required: "Email is required" })} />
                            {errors.email && <small style={{ color: "red" }}>{errors.email.message}</small>}
                        </div>

                        <div>
                            <label>Password</label><br />
                            <input
                                type="password"
                                {...register("password", {
                                    required: "Password is required",
                                    minLength: { value: 6, message: "Min 6 chars" },
                                })}
                            />
                            {errors.password && <small style={{ color: "red" }}>{errors.password.message}</small>}
                        </div>

                        <div>
                            <label>Role</label><br />
                            <select {...register("role", { required: true })}>
                                <option value="Customer">Customer</option>
                                <option value="Staff">Staff</option>
                                <option value="Admin">Admin</option>
                            </select>
                        </div>

                        <div>
                            <label>Phone (optional)</label><br />
                            <input
                                {...register("phone", {
                                    pattern: {
                                        value: /^\+?[0-9\s\-()]+$/,
                                        message: "Digits with optional +, space, -, ()",
                                    },
                                })}
                            />
                            {errors.phone && <small style={{ color: "red" }}>{errors.phone.message}</small>}
                        </div>

                        <div>
                            <label>Username (optional)</label><br />
                            <input
                                {...register("username", {
                                    maxLength: { value: 50, message: "Max 50 chars" },
                                    pattern: { value: /^[a-zA-Z0-9_.-]+$/, message: "Only letters, numbers, . _ -" },
                                })}
                            />
                            {errors.username && <small style={{ color: "red" }}>{errors.username.message}</small>}
                        </div>
                    </div>
                </fieldset>

                <fieldset style={{ border: "1px solid #ddd", padding: 12 }}>
                    <legend>Address (optional)</legend>
                    <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 12 }}>
                        <div>
                            <label>Street</label><br />
                            <input placeholder="e.g., 123 Main St, Apt 4B" {...register("address.street")} />
                        </div>
                        <div>
                            <label>City</label><br />
                            <input {...register("address.city")} />
                        </div>
                        <div>
                            <label>State</label><br />
                            <input {...register("address.state")} />
                        </div>
                        <div>
                            <label>Postal Code</label><br />
                            <input {...register("address.postalCode")} />
                        </div>
                        <div>
                            <label>Country</label><br />
                            <input {...register("address.country")} />
                        </div>
                    </div>
                </fieldset>

                <div style={{ marginTop: 16 }}>
                    <button type="submit" disabled={loading}>
                        {loading ? "Creating..." : "Create User"}
                    </button>
                </div>
            </form>

            {error && <p style={{ color: "red", marginTop: 12 }}>{error}</p>}

            {created && (
                <div style={{ marginTop: 16, padding: 12, border: "1px solid #ddd", borderRadius: 8 }}>
                    <b>User created:</b>
                    <pre style={{ margin: 0 }}>{JSON.stringify(created, null, 2)}</pre>
                </div>
            )}
        </div>
    );
}
*/

// src/pages/admin/CreateUser.tsx
import { useForm } from "react-hook-form";
import { useState } from "react";
import axios, { type AxiosError } from "axios";
import {
    createUser,
    type CreateUserReq,
    type UserDto,
    type AddressDto,
} from "../../services/userService";

/* ---------- helpers ---------- */
function sanitizeAddress(addr?: AddressDto): AddressDto | undefined {
    if (!addr) return undefined;

    const trimmed: AddressDto = {
        street: addr.street?.trim() ?? "",
        city: addr.city?.trim() ?? "",
        state: addr.state?.trim() ?? "",
        postalCode: addr.postalCode?.trim() ?? "",
        country: addr.country?.trim() ?? "",
    };

    // if all address fields are empty -> don't send address
    const allEmpty = Object.values(trimmed).every((v) => !v);
    if (allEmpty) return undefined;

    // if any field provided, Street must be present (backend requires it)
    if (!trimmed.street) {
        throw new Error("Street is required when you provide any address field.");
    }

    return trimmed;
}

function extractApiError(e: AxiosError<any>): string {
    const data = e.response?.data;
    if (!data) return e.message;
    if (typeof data.detail === "string" && data.detail) return data.detail;

    if (data.errors && typeof data.errors === "object" && !Array.isArray(data.errors)) {
        const msgs: string[] = [];
        for (const [k, v] of Object.entries<any>(data.errors)) {
            const list = Array.isArray(v) ? v : [v];
            for (const m of list) msgs.push(`${k}: ${m}`);
        }
        if (msgs.length) return msgs.join(" | ");
    }

    if (Array.isArray(data.errors)) {
        const msgs = data.errors
            .map((x: any) => x?.errorMessage || x?.message || String(x))
            .filter(Boolean);
        if (msgs.length) return msgs.join(" | ");
    }

    if (data.title) return data.title;
    if (data.message) return data.message;
    return e.message;
}

/* ---------- component ---------- */
export default function CreateUser() {
    type FormModel = Omit<CreateUserReq, "address"> & { address?: AddressDto };

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<FormModel>({
        defaultValues: {
            role: "Customer",
            address: { street: "", city: "", state: "", postalCode: "", country: "" },
        },
    });

    const [loading, setLoading] = useState(false);
    const [created, setCreated] = useState<UserDto | null>(null);
    const [error, setError] = useState<string | null>(null);

    const onSubmit = async (data: FormModel) => {
        try {
            setLoading(true);
            setError(null);
            setCreated(null);

            const payload: CreateUserReq = {
                firstName: data.firstName.trim(),
                lastName: data.lastName.trim(),
                email: data.email.trim(),
                password: data.password,
                role: data.role,
                phone: data.phone?.trim() || undefined,
                username: data.username?.trim() || undefined,
                address: sanitizeAddress(data.address),
            };

            const user = await createUser(payload);
            setCreated(user);

            reset({
                firstName: "",
                lastName: "",
                email: "",
                password: "",
                role: "Customer",
                phone: "",
                username: "",
                address: { street: "", city: "", state: "", postalCode: "", country: "" },
            });
        } catch (e: unknown) {
            if (e instanceof Error && e.message.includes("Street is required")) {
                setError(e.message);
            } else if (axios.isAxiosError(e)) {
                setError(extractApiError(e));
            } else {
                setError("Unexpected error");
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container mx-auto max-w-4xl p-6">
            {/* header */}
            <div className="mb-6 flex items-center justify-between">
                <div>
                    <h2 className="text-2xl font-semibold tracking-tight">Create User</h2>
                    <p className="mt-1 text-sm text-gray-500">Admin • add a new user account</p>
                </div>
                <span className="rounded-full bg-indigo-50 px-3 py-1 text-xs font-medium text-indigo-700">
          Car Rental System
        </span>
            </div>

            {/* error alert */}
            {error && (
                <div className="mb-4 rounded-lg border border-red-200 bg-red-50 p-3 text-red-700">
                    <div className="flex items-start gap-2">
                        <svg className="mt-0.5 h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                            <path
                                fillRule="evenodd"
                                d="M10 18a8 8 0 100-16 8 8 0 000 16zm.75-11.5a.75.75 0 00-1.5 0v4a.75.75 0 001.5 0v-4zM10 13a1 1 0 100 2 1 1 0 000-2z"
                                clipRule="evenodd"
                            />
                        </svg>
                        <p className="text-sm">{error}</p>
                    </div>
                </div>
            )}

            {/* success block */}
            {created && (
                <div className="mb-4 rounded-lg border border-green-200 bg-green-50 p-4">
                    <p className="mb-2 text-sm font-medium text-green-800">User created successfully</p>
                    <pre className="max-h-48 overflow-auto rounded-md bg-gray-900 p-3 text-xs text-gray-100">
            {JSON.stringify(created, null, 2)}
          </pre>
                </div>
            )}

            {/* form */}
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                {/* Basic */}
                <fieldset className="rounded-2xl border border-gray-200 bg-white p-6 shadow-sm">
                    <legend className="px-2 text-sm font-semibold text-gray-700">Basic</legend>

                    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                        <div>
                            <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                                First Name <span className="text-red-600">*</span>
                            </label>
                            <input
                                id="firstName"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("firstName", { required: "First name is required" })}
                            />
                            {errors.firstName && <p className="mt-1 text-sm text-red-600">{errors.firstName.message}</p>}
                        </div>

                        <div>
                            <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                                Last Name <span className="text-red-600">*</span>
                            </label>
                            <input
                                id="lastName"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("lastName", { required: "Last name is required" })}
                            />
                            {errors.lastName && <p className="mt-1 text-sm text-red-600">{errors.lastName.message}</p>}
                        </div>

                        <div>
                            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
                                Email <span className="text-red-600">*</span>
                            </label>
                            <input
                                id="email"
                                type="email"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("email", { required: "Email is required" })}
                            />
                            {errors.email && <p className="mt-1 text-sm text-red-600">{errors.email.message}</p>}
                        </div>

                        <div>
                            <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                                Password <span className="text-red-600">*</span>
                            </label>
                            <input
                                id="password"
                                type="password"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("password", {
                                    required: "Password is required",
                                    minLength: { value: 6, message: "Min 6 chars" },
                                })}
                            />
                            {errors.password && <p className="mt-1 text-sm text-red-600">{errors.password.message}</p>}
                        </div>

                        <div>
                            <label htmlFor="role" className="block text-sm font-medium text-gray-700">
                                Role <span className="text-red-600">*</span>
                            </label>
                            <select
                                id="role"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("role", { required: true })}
                            >
                                <option value="Customer">Customer</option>
                                <option value="Staff">Staff</option>
                                <option value="Admin">Admin</option>
                            </select>
                        </div>

                        <div>
                            <label htmlFor="phone" className="block text-sm font-medium text-gray-700">
                                Phone (optional)
                            </label>
                            <input
                                id="phone"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("phone", {
                                    pattern: {
                                        value: /^\+?[0-9\s\-()]+$/,
                                        message: "Digits with optional +, space, -, ()",
                                    },
                                })}
                            />
                            {errors.phone && <p className="mt-1 text-sm text-red-600">{errors.phone.message}</p>}
                        </div>

                        <div className="sm:col-span-2">
                            <label htmlFor="username" className="block text-sm font-medium text-gray-700">
                                Username (optional)
                            </label>
                            <input
                                id="username"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("username", {
                                    maxLength: { value: 50, message: "Max 50 chars" },
                                    pattern: { value: /^[a-zA-Z0-9_.-]+$/, message: "Only letters, numbers, . _ -" },
                                })}
                            />
                            {errors.username && <p className="mt-1 text-sm text-red-600">{errors.username.message}</p>}
                        </div>
                    </div>
                </fieldset>

                {/* Address */}
                <fieldset className="rounded-2xl border border-gray-200 bg-white p-6 shadow-sm">
                    <legend className="px-2 text-sm font-semibold text-gray-700">Address (optional)</legend>

                    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                        <div className="sm:col-span-2">
                            <label htmlFor="street" className="block text-sm font-medium text-gray-700">
                                Street
                            </label>
                            <input
                                id="street"
                                placeholder="e.g., 123 Main St, Apt 4B"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("address.street")}
                            />
                        </div>

                        <div>
                            <label htmlFor="city" className="block text-sm font-medium text-gray-700">
                                City
                            </label>
                            <input
                                id="city"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("address.city")}
                            />
                        </div>

                        <div>
                            <label htmlFor="state" className="block text-sm font-medium text-gray-700">
                                State
                            </label>
                            <input
                                id="state"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("address.state")}
                            />
                        </div>

                        <div>
                            <label htmlFor="postalCode" className="block text-sm font-medium text-gray-700">
                                Postal Code
                            </label>
                            <input
                                id="postalCode"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("address.postalCode")}
                            />
                        </div>

                        <div>
                            <label htmlFor="country" className="block text-sm font-medium text-gray-700">
                                Country
                            </label>
                            <input
                                id="country"
                                className="mt-1 block w-full rounded-lg border-gray-300 bg-white px-3 py-2 text-sm shadow-sm ring-1 ring-inset ring-gray-300 focus:border-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                {...register("address.country")}
                            />
                        </div>
                    </div>
                </fieldset>

                {/* submit */}
                <div className="flex items-center gap-3">
                    <button
                        type="submit"
                        disabled={loading}
                        className="inline-flex items-center gap-2 rounded-lg bg-indigo-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:opacity-50"
                    >
                        {loading && (
                            <svg className="h-5 w-5 animate-spin" viewBox="0 0 24 24" fill="none">
                                <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" />
                                <path
                                    className="opacity-75"
                                    fill="currentColor"
                                    d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z"
                                />
                            </svg>
                        )}
                        {loading ? "Creating..." : "Create User"}
                    </button>
                    <span className="text-xs text-gray-500">Fields marked with <span className="text-red-600">*</span> are required.</span>
                </div>
            </form>
        </div>
    );
}

