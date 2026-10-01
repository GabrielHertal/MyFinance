import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";

function Register() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    async function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        setError(null);
        setLoading(true);

        try {
            /* PENDENTE DE DESENVOLVIMENTO*/ 
            navigate("/");
        } catch {
            setError("Não foi possível criar sua conta.");
        } finally {
            setLoading(false);
        }
    }

    return (
        <main className="min-vh-100 bg-dark text-light">
            <div className="container min-vh-100">
                <div className="row min-vh-100 align-items-center justify-content-center">
                    <div className="col-lg-5 d-none d-lg-block pe-5">
                        <div className="mb-4">
                            <div className="bg-primary rounded-3 d-flex align-items-center justify-content-center mb-4"
                                 style={{ width: "56px", height: "56px" }}>
                                <i className="bi bi-wallet2 fs-3 text-white"></i>
                            </div>
                            <h1 className="display-4 fw-bold mb-3">
                                My<span className="text-primary">Finance</span>
                            </h1>
                            <p className="fs-5 text-secondary">
                                Controle suas finanças de forma simples,
                                organizada e inteligente.
                            </p>
                        </div>
                        <div className="mt-5">
                            <div className="d-flex align-items-center gap-3 mb-4">
                                <i className="bi bi-check-circle-fill text-primary fs-5"></i>
                                <span className="text-secondary">
                                    Acompanhe suas receitas e despesas
                                </span>
                            </div>
                            <div className="d-flex align-items-center gap-3 mb-4">
                                <i className="bi bi-check-circle-fill text-primary fs-5"></i>
                                <span className="text-secondary">
                                    Organize suas contas
                                </span>
                            </div>
                            <div className="d-flex align-items-center gap-3">
                                <i className="bi bi-check-circle-fill text-primary fs-5"></i>
                                <span className="text-secondary">
                                    Tenha uma visão clara do seu dinheiro
                                </span>
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-md-8 col-lg-5 col-xl-4">
                        <div className="card border-secondary shadow-lg rounded-4"
                             data-bs-theme="dark" >
                            <div className="card-body p-4 p-md-5">
                                <div className="d-lg-none text-center mb-4">
                                    <i className="bi bi-wallet2 text-primary fs-1"></i>
                                    <h2 className="fw-bold mt-2">
                                        MyFinance
                                    </h2>
                                </div>
                                <div className="mb-4">
                                    <h2 className="fw-bold mb-2">
                                        Crie sua conta
                                    </h2>
                                    <p className="text-secondary mb-0">
                                        Preencha seus dados para começar.
                                    </p>
                                </div>
                                {error && (
                                    <div
                                        className="alert alert-danger"
                                        role="alert"
                                    >
                                        <i className="bi bi-exclamation-circle me-2"></i>
                                        {error}
                                    </div>
                                )}
                                <form onSubmit={handleSubmit}>
                                    <div className="mb-3">
                                        <label htmlFor="name"
                                               className="form-label">
                                            Nome
                                        </label>
                                        <div className="input-group">
                                            <span className="input-group-text">
                                                <i className="bi bi-person"></i>
                                            </span>
                                            <input
                                                type="text"
                                                id="name"
                                                className="form-control"
                                                placeholder="Seu nome completo"
                                                value={name}
                                                onChange={(e) =>
                                                    setName(e.target.value)
                                                }
                                                required
                                            />
                                        </div>
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="email"
                                               className="form-label">
                                            E-mail
                                        </label>
                                        <div className="input-group">
                                            <span className="input-group-text">
                                                <i className="bi bi-envelope"></i>
                                            </span>
                                            <input
                                                type="email"
                                                id="email"
                                                className="form-control"
                                                placeholder="seu@email.com"
                                                value={email}
                                                onChange={(e) =>
                                                    setEmail(e.target.value)
                                                }
                                                required/>
                                        </div>
                                    </div>
                                    <div className="mb-2">
                                        <label
                                            htmlFor="password"
                                            className="form-label">
                                            Senha
                                        </label>
                                        <div className="input-group">
                                            <span className="input-group-text">
                                                <i className="bi bi-lock"></i>
                                            </span>
                                            <input
                                                type={
                                                    showPassword
                                                        ? "text"
                                                        : "password"
                                                }
                                                id="password"
                                                className="form-control"
                                                placeholder="Crie uma senha"
                                                value={password}
                                                onChange={(e) =>
                                                    setPassword(e.target.value)
                                                }
                                                minLength={6}
                                                required
                                            />
                                            <button
                                                type="button"
                                                className="btn btn-outline-secondary"
                                                onClick={() =>
                                                    setShowPassword(
                                                        !showPassword
                                                    )
                                                }
                                            >
                                                <i
                                                    className={`bi ${
                                                        showPassword
                                                            ? "bi-eye-slash"
                                                            : "bi-eye"
                                                    }`}
                                                ></i>
                                            </button>
                                        </div>
                                    </div>
                                    <div className="form-text mb-4">
                                        Mínimo de 6 caracteres.
                                    </div>
                                    <button
                                        type="submit"
                                        className="btn btn-primary w-100 py-2 fw-semibold"
                                        disabled={loading}
                                    >
                                        {loading ? (
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2"></span>
                                                Criando conta...
                                            </>
                                        ) : (
                                            <>
                                                Criar minha conta
                                                <i className="bi bi-arrow-right ms-2"></i>
                                            </>
                                        )}
                                    </button>
                                </form>
                                <div className="text-center mt-4">
                                    <span className="text-secondary">
                                        Já possui uma conta?
                                    </span>
                                    <Link
                                        to="/"
                                        className="text-primary text-decoration-none fw-semibold ms-1"
                                    >
                                        Entrar
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
export default Register;