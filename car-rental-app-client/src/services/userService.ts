import api from "./apiClient";

export type Role = "Admin" | "Customer" | "Staff";

export interface AddressDto {
    street: string;
    city: string;
    state: string;
    postalCode: string;
    country: string;
}

export interface CreateUserReq {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    role: Role;
    phone?: string;
    username?: string;
    address?: AddressDto; // must use 'street' shape when present
}

export interface UserDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
}

export async function createUser(payload: CreateUserReq): Promise<UserDto> {
    const res = await api.post("/api/v1/Users", payload);
    return res.data as UserDto;
}
