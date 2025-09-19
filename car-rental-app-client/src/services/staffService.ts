import api from "./apiClient";
import type { AddressDto } from "./userService"; // reuse same AddressDto (street, city, ...)

// ------- Types -------
export interface StaffDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: "Staff";
    phone?: string;
    username?: string;
    address?: AddressDto;
}

export interface CreateStaffReq {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    phone?: string;
    username?: string;
    address?: AddressDto;
    // role not required in body if your StaffController sets it; if needed we'll add "role: 'Staff'"
}

export interface UpdateStaffReq extends Partial<Omit<CreateStaffReq, "password">> {
    password?: string; // only if your API allows updating it
}

export interface PagedResult<T> {
    items: T[];
    page: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}

// ------- Endpoints -------
const BASE = "/api/v1/Staff"; // <-- if your controller is StaffsController, change to "/api/v1/Staffs"

export async function createStaff(payload: CreateStaffReq): Promise<StaffDto> {
    // If your backend expects role, uncomment:
    // const res = await api.post(BASE, { ...payload, role: "Staff" });
    const res = await api.post(BASE, payload);
    return res.data as StaffDto;
}

export async function getStaffPaged(page = 1, pageSize = 20): Promise<PagedResult<StaffDto>> {
    const res = await api.get(BASE, { params: { page, pageSize } });
    return res.data as PagedResult<StaffDto>;
}

export async function getStaffById(id: string): Promise<StaffDto> {
    const res = await api.get(`${BASE}/${id}`);
    return res.data as StaffDto;
}

export async function updateStaff(id: string, payload: UpdateStaffReq): Promise<StaffDto> {
    const res = await api.put(`${BASE}/${id}`, payload);
    return res.data as StaffDto;
}

export async function softDeleteStaff(id: string): Promise<void> {
    await api.delete(`${BASE}/${id}`);
}
