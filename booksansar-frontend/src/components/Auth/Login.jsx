// src/components/Auth/Login.jsx
import { useState, useEffect } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import authService from '../../services/authService';

function Login() {
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    });
    const [error, setError] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        if (location.state?.registrationSuccess) {
            alert('Registration successful! Please login.');
        }
    }, [location.state]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setIsSubmitting(true);

        try {
            await authService.login(formData.email, formData.password);
            navigate('/dashboard');
        } catch (error) {
            setError(
                error.response?.data?.message ||
                error.message ||
                'Login failed. Please check your credentials.'
            );
        } finally {
            setIsSubmitting(false);
        }
    };

    const containerStyle = {
        maxWidth: '400px',
        margin: '80px auto',
        padding: '40px',
        border: '1px solid #e0e0e0',
        borderRadius: '8px',
        boxShadow: '0 4px 12px rgba(0,0,0,0.05)',
        backgroundColor: '#fff',
        fontFamily: 'Arial, sans-serif',
        textAlign: 'center'
    };

    const inputStyle = {
        width: '100%',
        padding: '12px',
        marginBottom: '20px',
        border: '1px solid #ddd',
        borderRadius: '6px',
        fontSize: '16px',
        boxSizing: 'border-box'
    };

    const buttonStyle = {
        width: '100%',
        padding: '12px',
        backgroundColor: '#1976d2',
        color: 'white',
        border: 'none',
        borderRadius: '6px',
        fontSize: '16px',
        cursor: 'pointer',
        marginBottom: '20px',
        transition: 'background-color 0.2s',
        opacity: isSubmitting ? 0.7 : 1,
        ':hover': {
            backgroundColor: '#1565c0'
        }
    };

    const linkStyle = {
        color: '#1976d2',
        textDecoration: 'none',
        fontWeight: 'bold',
        ':hover': {
            textDecoration: 'underline'
        }
    };

    const errorStyle = {
        color: '#dc3545',
        marginBottom: '15px',
        fontSize: '14px'
    };

    return (
        <form onSubmit={handleSubmit} style={containerStyle}>
            <h2 style={{ marginBottom: '30px', color: '#333' }}>Sign In</h2>

            {error && <div style={errorStyle}>{error}</div>}

            <input
                type="email"
                placeholder="Email *"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                style={inputStyle}
                required
            />

            <div style={{ position: 'relative' }}>
                <input
                    type={showPassword ? "text" : "password"}
                    placeholder="Password *"
                    value={formData.password}
                    onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                    style={inputStyle}
                    required
                />
                <button
                    type="button"
                    onClick={() => setShowPassword(!showPassword)}
                    style={{
                        position: 'absolute',
                        right: '10px',
                        top: '12px',
                        background: 'none',
                        border: 'none',
                        cursor: 'pointer',
                        color: '#666',
                        fontSize: '14px'
                    }}
                >
                    {showPassword ? 'Hide' : 'Show'}
                </button>
            </div>

            <button
                type="submit"
                style={buttonStyle}
                disabled={isSubmitting}
            >
                {isSubmitting ? 'Signing In...' : 'Sign In'}
            </button>

            <p style={{ color: '#666' }}>
                Don't have an account?{' '}
                <Link to="/register" style={linkStyle}>
                    Register here
                </Link>
            </p>
        </form>
    );
}

export default Login;