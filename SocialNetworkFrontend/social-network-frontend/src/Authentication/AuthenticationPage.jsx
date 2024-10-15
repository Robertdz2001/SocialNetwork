import classes from './AuthenticationPage.module.scss';
import LoginComponent from './Components/LoginComponent/LoginComponent';
import RegisterComponent from './Components/RegisterComponent/RegisterComponent';
import PasswordResetComponent from './Components/PasswordResetComponent/PasswordResetComponent';
import { useState } from 'react';
import { baseUrl } from '../Shared/Options/ApiOptions';

function AuthenticationPage() {
    const [ShowedComponent, setShowedComponent] = useState("Login");
    const userId = 12;

    const changeShowedComponent = (componentName) => {
        setShowedComponent(componentName);
    }


    return (
        <div className={classes['auth-page-wrapper']}>
        <div>
            {/* <img src={`${baseUrl}/user/${userId}/profile-picture`} alt="Profile" width="150" height="150" /> */}
        </div>
            <div className={classes['auth-container']}>
                {ShowedComponent === "Login" && (
                    <LoginComponent
                        changeShowedComponent={changeShowedComponent}
                    />
                )}
                {ShowedComponent === "Register" && (
                    <RegisterComponent
                        changeShowedComponent={changeShowedComponent}
                    />
                )}
                {ShowedComponent === "PasswordReset" && (
                    <PasswordResetComponent
                        changeShowedComponent={changeShowedComponent}
                    />
                )}
            </div>
        </div>
    );
}

export default AuthenticationPage;