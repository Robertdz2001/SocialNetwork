import authClasses from '../../AuthenticationPage.module.scss';
import { useState, useRef } from "react";
import axios from "axios";
import { baseUrl } from '../../../Shared/Options/ApiOptions';

function RegisterComponent(props) {
    const emailErrRef = useRef(null);
    const tokenErrRef = useRef(null);
    const passwordErrRef = useRef(null);
    const firstNameErrRef = useRef(null);
    const lastNameErrRef = useRef(null);
    const phoneErrRef = useRef(null);

    const [IsVerified, setIsVerified] = useState(false);
    const [emailState, setEmailState] = useState("");
    const [profilePicture, setProfilePicture] = useState(null);
    const [picturePreview, setPicturePreview] = useState(null);

    const handleVerify = async (e) => {
        e.preventDefault();
        const email = e.target.email.value.trim();

        // Check for valid email format
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (email === "") {
            emailErrRef.current.innerHTML = "Email can not be empty.";
            return;
        } else if (!emailRegex.test(email)) {
            emailErrRef.current.innerHTML = "Email is not valid.";
            return;
        } else {
            emailErrRef.current.innerHTML = "";
        }

        const data = {
            email: email
        }

        try {
            // Register new user
            await axios.post(`${baseUrl}/user/verify-register`, data);
            setEmailState(data.email);
            setIsVerified(true);
        } catch (err) {
            // Display backend exeptions
            if (err.response && err.response.data) {
                emailErrRef.current.innerHTML = err.response.data;
            }
        }
    };

    const handleProfilePictureChange = (e) => {
        const file = e.target.files[0];
        if (file && (file.type === "image/jpeg" || file.type === "image/png" || file.type === "image/jpg")) {
            setProfilePicture(file);
            setPicturePreview(URL.createObjectURL(file));  // Podgląd zdjęcia
        }
    };

    const handleCreate = async (e) => {
        e.preventDefault();

        const data = {
            email: emailState,
            token: e.target.token.value,
            password: e.target.password.value,
            firstName: e.target.firstName.value,
            lastName: e.target.lastName.value,
            phoneNumber: e.target.phoneNumber.value,
            country: e.target.country.value,
            city: e.target.city.value,
            profilePicture: profilePicture
        }

        // Regex for password validation (minimum 8 characters, one uppercase, one lowercase, one digit, one special character)
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$/;
        // Regex for phone number validation (simple international format)
        const phoneRegex = /^\+?[1-9]\d{1,14}$/;

        let hasErrors = false;

        // Validate token
        if (!data.token) {
            tokenErrRef.current.innerHTML = "Token is required.";
            hasErrors = true;
        } else {
            tokenErrRef.current.innerHTML = "";
        }

        // Validate password
        if (!passwordRegex.test(data.password)) {
            passwordErrRef.current.innerHTML = "Password must be at least 8 characters long, with at least one uppercase, one lowercase, one digit, and one special character.";
            hasErrors = true;
        } else {
            passwordErrRef.current.innerHTML = "";
        }

        // Validate first name
        if (!data.firstName) {
            firstNameErrRef.current.innerHTML = "First name is required.";
            hasErrors = true;
        } else {
            firstNameErrRef.current.innerHTML = "";
        }

        // Validate last name
        if (!data.lastName) {
            lastNameErrRef.current.innerHTML = "Last name is required.";
            hasErrors = true;
        } else {
            lastNameErrRef.current.innerHTML = "";
        }

        // Validate phone number
        if (!phoneRegex.test(data.phoneNumber)) {
            phoneErrRef.current.innerHTML = "Phone number is invalid. It should be in international format. For example +48123456789";
            hasErrors = true;
        } else {
            phoneErrRef.current.innerHTML = "";
        }

        if (hasErrors) {
            return;
        }

        try {
            // Register new user
            await axios.post(`${baseUrl}/user/register`, data, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            handleLogIn();
            alert("Account created. You can sign in now.");
        } catch (err) {
            // Display backend exeptions
            if (err.response && err.response.data) {
                tokenErrRef.current.innerHTML = err.response.data;
            }
        }
    };

    const handleCancel = (e) => {
        setIsVerified(false);
    };

    const handleLogIn = (e) => {
        props.changeShowedComponent("Login")
    };

    return (
        <>
            {!IsVerified && (
                <>
                    <h2 className={authClasses['auth-header']}>Sign up</h2>
                    <form onSubmit={handleVerify} className={authClasses['auth-form']}>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="email">Email:</label>
                            <input className={authClasses['auth-input']} type="text" id="email" name="email" placeholder="Enter email" />
                            <span ref={emailErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className='row justify-content-between'>
                            <div className='col-5'>
                                <button onClick={handleLogIn} className={authClasses['auth-primary-btn']}>
                                    Cancel
                                </button>
                            </div>
                            <div className='col-5'>
                                <button type='submit' className={authClasses['auth-primary-btn']}>
                                    Sign up
                                </button>
                            </div>
                        </div>
                    </form>
                </>
            )}
            {IsVerified && (
                <>
                    <h2 className={authClasses['auth-header']}>Create account</h2>
                    <form onSubmit={handleCreate} className={authClasses['auth-form']}>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="token">We have sent a verification token to the provided email. Token will expire in 30 minutes.</label>
                            <input className={authClasses['auth-input']} type="number" id="token" name="token" placeholder="Enter token" required/>
                            <span ref={tokenErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="profilePicture">Profile Picture</label>
                            <input className={authClasses['auth-input']} type="file" accept="image/jpeg,image/png,image/jpg" onChange={handleProfilePictureChange} required/>
                            {picturePreview && <img src={picturePreview} alt="Profile Preview" className={authClasses['profile-preview']} />}
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="password">Password</label>
                            <input className={authClasses['auth-input']} type="password" id="password" name="password" placeholder="Enter password" required/>
                            <span ref={passwordErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="firstName">First name</label>
                            <input className={authClasses['auth-input']} type="text" id="firstName" name="firstName" placeholder="Enter first name" maxLength={20} required/>
                            <span ref={firstNameErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="lastName">Last name</label>
                            <input className={authClasses['auth-input']} type="text" id="lastName" name="lastName" placeholder="Enter last name" maxLength={20} required/>
                            <span ref={lastNameErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="phoneNumber">Phone number</label>
                            <input className={authClasses['auth-input']} type="tel" id="phoneNumber" name="phoneNumber" placeholder="Enter phone number" maxLength={30} required/>
                            <span ref={phoneErrRef} className={authClasses['auth-error-message']}></span>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="country">Country</label>
                            <input className={authClasses['auth-input']} type="text" id="country" name="country" placeholder="Enter country" maxLength={30} required/>
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="city">City</label>
                            <input className={authClasses['auth-input']} type="text" id="city" name="city" placeholder="Enter city" maxLength={30} required/>
                        </div>
                        <div className='row justify-content-between'>
                            <div className='col-5'>
                                <button onClick={handleCancel} className={authClasses['auth-primary-btn']}>
                                    Cancel
                                </button>
                            </div>
                            <div className='col-5'>
                                <button type='submit' className={authClasses['auth-primary-btn']}>
                                    Verify and Create
                                </button>
                            </div>
                        </div>
                    </form>
                </>
            )}
        </>
    );
}

export default RegisterComponent;  