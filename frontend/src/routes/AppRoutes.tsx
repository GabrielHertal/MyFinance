import { Route, Routes } from "react-router-dom";
import Auth from '../features/auth/pages/LoginPage';
import Home from '../features/auth/pages/HomePage';

function AppRoutes() {
    return (
        <Routes>
            <Route path="/auth" element={<Auth />} />
            <Route path="/" element={<Home />} />
        </Routes>
    )
}