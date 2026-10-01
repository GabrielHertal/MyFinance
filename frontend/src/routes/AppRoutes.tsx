import { Navigate, Route, Routes } from "react-router-dom";
import Auth from '../features/auth/pages/LoginPage';
import Home from '../features/auth/pages/HomePage';
import type { ReactNode } from "react";
import Register from "../features/auth/pages/RegisterPage";

interface ProtectedRouteProps{
    children : ReactNode;
}

const ProtectedRoute = ({ children } : ProtectedRouteProps) => {
    const token = localStorage.getItem('AuthToken');
    return token ? children : <Navigate to="/auth" replace/>;
};

function AppRoutes() {
    return (
        <Routes>
            <Route path="/auth" element={<Auth />} />
            <Route path="/register" element={<Register />} />
            <Route path="/" element={<ProtectedRoute><Home /></ProtectedRoute>} />
        </Routes>
    );
};

export default AppRoutes;