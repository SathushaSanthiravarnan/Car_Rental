import { useForm } from "react-hook-form";
import { useState } from "react";
import axios, { type AxiosError } from "axios";
import { createStaff, type CreateStaffReq, type StaffDto } from "../../services/staffService";
import type { AddressDto } from "../../services/userService";

// helpers
function sanitizeAddress(addr?: AddressDto): AddressDto | undefined {
    if (!addr) return undefined;
    const trimmed: AddressDto = {
        street: addr.street?.trim() ?? "",
        city: addr.city?.trim() ?? "",
        state: addr.state?.trim() ?? "",
        postalCode: addr.postalCode?.trim() ?? "",
        country: addr.country?.trim() ?? "",
    };
    const allEmpty = Object.values(trimmed).every((v) => !v);
    if (allEmpty) return undefined;
    if (!trimmed.street) throw new Error("Street is required when you provide any address field.");
    return trimmed;
}

function extractApiError(e: AxiosError<any>): string {
    const data = e.response?.data;
    if (!data) return e.message;
    if (data.detail) return data.detail;
    if (data.errors && typeof data.errors === "object" && !Array.isArray(data.errors)) {
        const msgs: string[] = [];
        for (const [k, v] of Object.entries<any>(data.errors)) {
            const list = Array.isArray(v) ? v : [v];
            for (const m of list) msgs.push(`${k}: ${m}`);
        }
        if (msgs.length) return msgs.join(" | ");
    }
    if (Array.isArray(data.errors)) {
        const msgs = data.errors.map((x: any) => x?.errorMessage || x?.message || String(x)).filter(Boolean);
        if (msgs.length) return msgs.join(" | ");
    }
    return data.title || data.message || e.message;
}

type FormModel = CreateStaffReq; // same shape

export default function CreateStaff() {
    const { register, handleSubmit, reset, formState: { errors } } = useForm<FormModel>({
        defaultValues: {
            address: { street: "", city: "", state: "", postalCode: "", country: "" },
        },
    });

    const [loading, setLoading] = useState(false);
    const [created, setCreated] = useState<StaffDto | null>(null);
    const [error, setError] = useState<string | null>(null);

    const onSubmit = async (data: FormModel) => {
        try {
            setLoading(true);
            setError(null);
            setCreated(null);

            const payload: CreateStaffReq = {
                firstName: data.firstName.trim(),
                lastName: data.lastName.trim(),
                email: data.email.trim(),
                password: data.password,
                phone: data.phone?.trim() || undefined,
                username: data.username?.trim() || undefined,
                address: sanitizeAddress(data.address),
            };

            // If your API needs role explicitly:
            // const staff = await createStaff({ ...payload, role: "Staff" } as any);
            const staff = await createStaff(payload);
            setCreated(staff);

            reset({
                firstName: "",
                lastName: "",
                email: "",
                password: "",
                phone: "",
                username: "",
                address: { street: "", city: "", state: "", postalCode: "", country: "" },
            });
        } catch (e: unknown) {
            if (e instanceof Error && e.message.includes("Street is required")) setError(e.message);
            else if (axios.isAxiosError(e)) setError(extractApiError(e));
            else setError("Unexpected error");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container mx-auto max-w-4xl p-6">
            <div className="mb-6">
                <h2 className="text-2xl font-semibold tracking-tight underline">Create Staff</h2>
                <p className="mt-1 text-sm text-gray-500">Add a staff member to the system</p>
            </div>

            {error && (
                <div className="mb-4 rounded-lg border border-red-200 bg-red-50 p-3 text-red-700">
                    <p className="text-sm">{error}</p>
                </div>
            )}

            {created && (
                <div className="mb-4 rounded-lg border border-green-200 bg-green-50 p-4">
                    <p className="mb-2 text-sm font-medium text-green-800">Staff created</p>
                    <pre className="max-h-48 overflow-auto rounded-md bg-gray-900 p-3 text-xs text-gray-100">
            {JSON.stringify(created, null, 2)}
          </pre>
                </div>
            )}

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                <fieldset className="rounded-2xl border border-gray-200 bg-white p-6 shadow-sm">
                    <legend className="px-2 text-sm font-semibold text-gray-700">Basic</legend>

                    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                        <div>
                            <label className="block text-sm font-medium text-gray-700">First Name *</label>
                            <input
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("firstName", { required: "First name is required" })}
                            />
                            {errors.firstName && <p className="mt-1 text-sm text-red-600">{errors.firstName.message}</p>}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Last Name *</label>
                            <input
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("lastName", { required: "Last name is required" })}
                            />
                            {errors.lastName && <p className="mt-1 text-sm text-red-600">{errors.lastName.message}</p>}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Email *</label>
                            <input
                                type="email"
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("email", { required: "Email is required" })}
                            />
                            {errors.email && <p className="mt-1 text-sm text-red-600">{errors.email.message}</p>}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Password *</label>
                            <input
                                type="password"
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("password", { required: "Password is required", minLength: { value: 6, message: "Min 6 chars" } })}
                            />
                            {errors.password && <p className="mt-1 text-sm text-red-600">{errors.password.message}</p>}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Phone (optional)</label>
                            <input
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("phone", { pattern: { value: /^\+?[0-9\s\-()]+$/, message: "Digits with optional +, space, -, ()" } })}
                            />
                            {errors.phone && <p className="mt-1 text-sm text-red-600">{errors.phone.message}</p>}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Username (optional)</label>
                            <input
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("username", {
                                    maxLength: { value: 50, message: "Max 50 chars" },
                                    pattern: { value: /^[a-zA-Z0-9_.-]+$/, message: "Only letters, numbers, . _ -" },
                                })}
                            />
                            {errors.username && <p className="mt-1 text-sm text-red-600">{errors.username.message}</p>}
                        </div>
                    </div>
                </fieldset>

                <fieldset className="rounded-2xl border border-gray-200 bg-white p-6 shadow-sm">
                    <legend className="px-2 text-sm font-semibold text-gray-700">Address (optional)</legend>

                    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                        <div className="sm:col-span-2">
                            <label className="block text-sm font-medium text-gray-700">Street</label>
                            <input
                                placeholder="e.g., 123 Main St, Apt 4B"
                                className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                {...register("address.street")}
                            />
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">City</label>
                            <input className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500" {...register("address.city")} />
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">State</label>
                            <input className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500" {...register("address.state")} />
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Postal Code</label>
                            <input className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500" {...register("address.postalCode")} />
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700">Country</label>
                            <input className="mt-1 block w-full rounded-lg border-gray-300 px-3 py-2 text-sm shadow-sm focus:border-indigo-500 focus:ring-indigo-500" {...register("address.country")} />
                        </div>
                    </div>
                </fieldset>

                <button
                    type="submit"
                    disabled={loading}
                    className="inline-flex items-center gap-2 rounded-lg bg-indigo-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:opacity-50"
                >
                    {loading && (
                        <svg className="h-5 w-5 animate-spin" viewBox="0 0 24 24" fill="none">
                            <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" />
                            <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z" />
                        </svg>
                    )}
                    {loading ? "Creating..." : "Create Staff"}
                </button>
            </form>
        </div>
    );
}
