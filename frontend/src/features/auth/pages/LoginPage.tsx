import { useState} from "react"
import { Login } from "../../../api/Auth/api"
import { useNavigate } from "react-router-dom";

export function Auth () {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<Error | null>(null);
    const navigate = useNavigate();

    const handleSubmit = async () => {
        setLoading(true);
        setError(null);
        try
        {
            const response = await Login(email,password);
            if(response.status === 401)
            {
                alert("Usuário ou senha inválidos!");
                return;
            }
            if(response.token === undefined)
            {
                alert("Erro ao realizar login!");
                return; 
            }
            localStorage.setItem("AuthToken", response.token);
        }
        catch(erro)
        {
            if(erro instanceof Error){
                setError(erro);
            }
            console.error(error);
            setLoading(false);
        }
        finally
        {
            setLoading(true);
            navigate("/");
        }
    }

    return (
        <main className="container min-vh-100 d-flex align-items-center justify-content-center">
            <div className="card shadow p-4" style={{ width: "100%", maxWidth: "420" }}>
                <h1 className="h3 text-center mb-4">MyFinance</h1>
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label htmlFor="email" className="form-label">Email</label>
                        <input
                            type="email"
                            className="form-control"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="password" className="form-label">Password</label>
                        <input
                            type="password"
                            className="form-control"
                            id="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    <button 
                        type="submit" 
                        className="btn btn-primary w-100" 
                        disabled={loading}> {loading? "Entrando..." : "Entrar"}    
                    </button>
                </form>
            </div>
        </main>
    )
}

export default Auth;