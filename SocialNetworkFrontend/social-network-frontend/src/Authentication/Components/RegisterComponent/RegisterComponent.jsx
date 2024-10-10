import authClasses from '../../AuthenticationPage.module.scss';
import { useState, useRef } from "react";

function RegisterComponent(props) {
    const emailErrRef = useRef(null);
    const passwordErrRef = useRef(null);

    const [IsVerified, setIsVerified] = useState(false);

    const handleVerify = (e) => {
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

        setIsVerified(true);
    };

    const handleCreate = (e) => {
        e.preventDefault();
    };

    const handleCancel = (e) => {
        setIsVerified(false);
    };

    const handleLogIn = (e) => {
        e.preventDefault();
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
                    <form className={authClasses['auth-form']}>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="token">We have sent a verification token to the provided email. Token will expire in 30 minutes.</label>
                            <input className={authClasses['auth-input']} type="number" id="token" name="token" placeholder="Enter token" />
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="firstName">First name</label>
                            <input className={authClasses['auth-input']} type="text" id="firstName" name="firstName" placeholder="Enter first name" />
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="lastName">Last name</label>
                            <input className={authClasses['auth-input']} type="text" id="lastName" name="lastName" placeholder="Enter last name" />
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="phoneNumber">Phone number</label>
                            <input className={authClasses['auth-input']} type="text" id="phoneNumber" name="phoneNumber" placeholder="Enter phone number" />
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="country">Country</label>
                            <input className={authClasses['auth-input']} type="text" id="country" name="country" placeholder="Enter country" />
                        </div>
                        <div className={authClasses['auth-input-group']}>
                            <label className={authClasses['auth-label']} htmlFor="city">City</label>
                            <input className={authClasses['auth-input']} type="text" id="city" name="city" placeholder="Enter city" />
                        </div>
                        <div className='row justify-content-between'>
                            <div className='col-5'>
                                <button onClick={handleCancel} className={authClasses['auth-primary-btn']}>
                                    Cancel
                                </button>
                            </div>
                            <div className='col-5'>
                                <button type='submit' onClick={handleCreate} className={authClasses['auth-primary-btn']}>
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