import { Routes, Route, Link } from "react-router-dom";
import CreateUser from "../pages/admin/CreateUser";
import CreateStaff from "../pages/admin/CreateStaff";

function Home() {
    return (
        <div className="container mx-auto max-w-xl p-6">
            <h3 className="text-xl font-semibold mb-3">Home</h3>
            <ul className="list-disc pl-5 space-y-2">
                <li>
                    <Link className="text-indigo-600 hover:underline" to="/admin/users/create">
                        Go to Create User
                    </Link>
                </li>
                <li>
                    <Link className="text-indigo-600 hover:underline" to="/admin/staff/create">
                        Go to Create Staff
                    </Link>
                </li>
            </ul>
        </div>
    );
}

function NotFound() {
    return <h3 className="p-6 text-red-600">404 - Not Found</h3>;
}

export default function AppRoutes() {
    return (
        <Routes>
            <Route index element={<Home />} />
            <Route path="/admin/users/create" element={<CreateUser />} />
            <Route path="/admin/staff/create" element={<CreateStaff />} />
            <Route path="*" element={<NotFound />} />
        </Routes>
    );
}
