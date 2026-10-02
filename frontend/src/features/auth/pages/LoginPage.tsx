import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../services/AuthService";
import type { LoginRequest } from "../types/AuthTypes";
import axios from "axios";

export function Auth() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const navigate = useNavigate();

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setLoading(true);
        setError(null);

        const request: LoginRequest = {email,password};

        try 
        {
            const response = await login(request);
            if (!response.accessToken) {
                setError("Não foi possível realizar o login.");
                return;
            }
            localStorage.setItem("AuthToken", response.accessToken);
            navigate("/");
        } 
        catch (erro) 
        {
            if(axios.isAxiosError(erro)) {
                if(erro.response?.data.code === 401) {
                    setError(erro.response?.data?.message);
                }
                else
                {
                    setError("Ocorreu um erro inesperado. Por favor, tente novamente.");
                    console.error(erro.response?.data);
                }
            }
            else
            {
                setError("Ocorreu um erro inesperado. Por favor, tente novamente.");
            }
        } 
        finally 
        {
            setLoading(false);
        }
    };

    return (
        <main className="min-vh-100 bg-dark text-light">
            <div className="container min-vh-100">
                <div className="row min-vh-100 align-items-center justify-content-center">
                    <div className="col-lg-5 d-none d-lg-block pe-5">
                        <div className="bg-primary rounded-3 d-flex align-items-center justify-content-center mb-4"
                             style={{
                                 width: "56px",
                                 height: "56px"
                             }}>
                            <i className="bi bi-wallet2 fs-3 text-white"></i>
                        </div>
                        <h1 className="display-4 fw-bold mb-3 text-light">
                            My<span className="text-primary">Finance</span>
                        </h1>
                        <p className="fs-5 text-secondary mb-5">
                            Seu dinheiro mais organizado,
                            suas decisões mais claras.
                        </p>
                        <div className="d-flex align-items-center gap-3 mb-4">
                            <i className="bi bi-graph-up-arrow text-primary fs-5"></i>
                            <div>
                                <div className="fw-semibold">
                                    Acompanhe seus gastos
                                </div>
                                <small className="text-secondary">
                                    Saiba exatamente para onde seu dinheiro está indo.
                                </small>
                            </div>
                        </div>
                        <div className="d-flex align-items-center gap-3 mb-4">
                            <i className="bi bi-pie-chart text-primary fs-5"></i>
                            <div>
                                <div className="fw-semibold">
                                    Organize suas finanças
                                </div>
                                <small className="text-secondary">
                                    Receitas, despesas e contas em um único lugar.
                                </small>
                            </div>
                        </div>
                        <div className="d-flex align-items-center gap-3">
                            <i className="bi bi-bullseye text-primary fs-5"></i>
                            <div>
                                <div className="fw-semibold">
                                    Alcance seus objetivos
                                </div>
                                <small className="text-secondary">
                                    Tenha uma visão clara da sua vida financeira.
                                </small>
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-8 col-lg-5 col-xl-4">
                        <div className="card border-secondary shadow-lg rounded-4" data-bs-theme="dark">
                            <div className="card-body p-4 p-md-5">
                                <div className="d-lg-none text-center mb-4">
                                    <i className="bi bi-wallet2 text-primary fs-1"></i>
                                    <h2 className="fw-bold mt-2">
                                        MyFinance
                                    </h2>
                                </div>
                                <div className="mb-4">
                                    <h2 className="fw-bold mb-2">
                                        Bem-vindo!
                                    </h2>
                                    <p className="text-secondary">
                                        Entre na sua conta para continuar.
                                    </p>
                                </div>
                                {error && (
                                    <div className="alert alert-danger d-flex align-items-center"role="alert">
                                        <i className="bi bi-exclamation-circle me-2"></i>
                                        {error}
                                    </div>
                                )}
                                <form onSubmit={handleSubmit}>
                                    <div className="mb-3">
                                        <label htmlFor="email"className="form-label">E-mail</label>
                                        <div className="input-group">
                                            <span className="input-group-text">
                                                <i className="bi bi-envelope"></i>
                                            </span>
                                            <input type="email"
                                                   id="email"
                                                   className="form-control"
                                                   placeholder="seu@email.com"
                                                   value={email}
                                                   onChange={(e) =>
                                                       setEmail(e.target.value)
                                                   }
                                                   required
                                                   autoComplete="email"/>    
                                        </div>
                                    </div>
                                    <div className="mb-2">
                                        <label htmlFor="password" className="form-label">Senha</label>
                                        <div className="input-group">
                                            <span className="input-group-text">
                                                <i className="bi bi-lock"></i>
                                            </span>
                                            <input type={
                                                   showPassword
                                                   ? "text"
                                                   : "password"
                                                   }
                                                   id="password"
                                                   className="form-control"
                                                   placeholder="Sua senha"
                                                   value={password}
                                                   onChange={(e) =>
                                                       setPassword(e.target.value)
                                                   }
                                                   required
                                                   autoComplete="current-password"/>
                                            <button type="button"
                                                    className="btn btn-outline-secondary"
                                                    onClick={() =>
                                                        setShowPassword(
                                                            !showPassword
                                                        )
                                                    }
                                                    aria-label={
                                                        showPassword
                                                            ? "Ocultar senha"
                                                            : "Mostrar senha"
                                                    }>
                                                <i className={`bi ${
                                                   showPassword
                                                    ? "bi-eye-slash"
                                                    : "bi-eye"
                                                   }`}>
                                                </i>
                                            </button>
                                        </div>
                                    </div>
                                    <div className="text-end mb-4">
                                        <a href="#" className="small text-decoration-none">
                                            Esqueceu sua senha?
                                        </a>
                                    </div>
                                    <button type="submit"
                                            className="btn btn-primary w-100 py-2 fw-semibold"
                                            disabled={loading}>
                                        {loading ? (
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>
                                                Entrando...
                                            </>
                                        ) : (
                                            <>
                                                Entrar
                                                <i className="bi bi-arrow-right ms-2"></i>
                                            </>
                                        )}
                                    </button>
                                </form>
                                <div className="text-center mt-4">
                                    <span className="text-secondary">
                                        Ainda não possui uma conta?
                                    </span>
                                    <Link to="/register" className="text-primary text-decoration-none fw-semibold ms-1">
                                        Cadastre-se
                                    </Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    );
}
export default Auth;