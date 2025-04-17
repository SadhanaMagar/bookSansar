// src/components/Auth/Register.jsx
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import authService from '../../services/authService';

function Register() {
    const [formData, setFormData] = useState({
        firstname: '',
        lastname: '',
        email: '',
        password: ''
    });
    const [error, setError] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setIsSubmitting(true);

        try {
            await authService.register(
                formData.firstname,
                formData.lastname,
                formData.email,
                formData.password
            );
            navigate('/login', { state: { registrationSuccess: true } });
        } catch (error) {
            setError(
                error.response?.data?.message ||
                error.message ||
                'Registration failed. Please try again.'
            );
        } finally {
            setIsSubmitting(false);
        }
    };

    const formStyle = {
        maxWidth: '400px',
        margin: '50px auto',
        padding: '40px',
        border: '1px solid #e0e0e0',
        borderRadius: '8px',
        boxShadow: '0 4px 12px rgba(0,0,0,0.05)',
        fontFamily: 'Arial, sans-serif',
        backgroundColor: '#fff',
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
        backgroundColor: '#007bff',
        color: '#fff',
        border: 'none',
        borderRadius: '6px',
        fontSize: '16px',
        cursor: 'pointer',
        marginTop: '10px',
        transition: 'background-color 0.2s',
        opacity: isSubmitting ? 0.7 : 1,
        ':hover': {
            backgroundColor: '#0069d9'
        }
    };

    const linkStyle = {
        color: '#007bff',
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
        <form onSubmit={handleSubmit} style={formStyle}>
            <h2 style={{ marginBottom: '30px', color: '#333' }}>Register</h2>

            {error && <div style={errorStyle}>{error}</div>}

            <div style={{ display: 'flex', gap: '15px' }}>
                <div style={{ flex: 1 }}>
                    <input
                        type="text"
                        placeholder="First Name"
                        value={formData.firstname}
                        onChange={(e) => setFormData({ ...formData, firstname: e.target.value })}
                        style={inputStyle}
                        required
                    />
                </div>
                <div style={{ flex: 1 }}>
                    <input
                        type="text"
                        placeholder="Last Name"
                        value={formData.lastname}
                        onChange={(e) => setFormData({ ...formData, lastname: e.target.value })}
                        style={inputStyle}
                        required
                    />
                </div>
            </div>

            <input
                type="email"
                placeholder="Email"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                style={inputStyle}
                required
            />

            <div style={{ position: 'relative' }}>
                <input
                    type={showPassword ? "text" : "password"}
                    placeholder="Password"
                    value={formData.password}
                    onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                    style={inputStyle}
                    required
                    minLength="6"
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

            <p style={{ fontSize: '12px', color: '#666', marginTop: '-15px', marginBottom: '20px', textAlign: 'left' }}>
                Password must be at least 6 characters
            </p>

            <button
                type="submit"
                style={buttonStyle}
                disabled={isSubmitting}
            >
                {isSubmitting ? 'Registering...' : 'Register'}
            </button>

            <p style={{ marginTop: '20px', color: '#666' }}>
                Already have an account?{' '}
                <Link to="/login" style={linkStyle}>
                    Login here
                </Link>
            </p>
        </form>
    );
}

export default Register;