import { Navigate, Route, Routes } from "react-router-dom";
import Auth from '../features/auth/pages/LoginPage';
import Home from '../features/auth/pages/HomePage';
import type { ReactNode } from "react";

interface ProtectedRouteProps{
    children : ReactNode;
}

const ProtectedRoute = ({ children } : ProtectedRouteProps) => {
    const token = localStorage.getItem('AuthToken');
    return token ? children : <Navigate to="/login" replace/>;
};

function AppRoutes() {
    return (
        <Routes>
            <Route path="/auth" element={<Auth />} />
            <Route path="/" element={<ProtectedRoute><Home /></ProtectedRoute>} />
        </Routes>
    );
};

export default AppRoutes;