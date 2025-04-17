import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import ProfileView from './Profile/ProfileView';
import ProfileEdit from './Profile/ProfileEdit';


function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/profile" element={<ProfileView />} />
                <Route path="/profile/edit" element={<ProfileEdit />} />
            </Routes>
        </Router>
    );
}

export default App;
