import './App.css'
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import AppRoutes from './routes/AppRoutes';

export default function App() {
  const navigate = useNavigate();
  useEffect(() => {
    const CheckToken = async () => {
      try
      { 
        if(window.location.pathname === '/register' ) return;
        const token = localStorage.getItem('AuthToken');
        if(!token)
        {
          navigate('/auth');
          return;
        }
        else
        {
          navigate('/');
        }
      }
      catch(error)
      {
        localStorage.removeItem('AuthToken');
        console.error("Erro ao validar Token:", error);
        navigate('/auth');
      }
    }
    CheckToken();
  }, [navigate]);
  return <AppRoutes />;
}