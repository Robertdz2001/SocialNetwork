import React from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import classes from './Layout.module.scss';
import { baseUrl, authorization } from './Shared/Options/ApiOptions';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { faHouse, faRightFromBracket, faUser } from '@fortawesome/free-solid-svg-icons';
import { faRocketchat } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const Layout = ({ isAuthenticated }) => {
    const [userData, setUserData] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getUserShortInfo = async () => {
            if (!isAuthenticated) {
                navigate('/auth');
                return;
            }

            try {
                const response = await axios.get(
                    `${baseUrl}/user/user-short-info`,
                    authorization(localStorage.getItem("token"))
                );

                setUserData(response.data);

            } catch (err) {
                alert(err.response.data);
            }
        };
        getUserShortInfo();
    }, []);

    const handleLogOut = () => {
        localStorage.removeItem("token");
        window.location.reload();
    }

    if (!userData) {
        return <div>Loading...</div>;
    }

    return (
        <div className={classes.container}>
            <header className={`${classes.header} d-flex align-items-center justify-content-between`}>
                <div className='m-0'>Social Network</div>
                <div className='m-0 d-flex g-2 align-item-center'>
                    <img src={`${baseUrl}/user/${userData.userId}/profile-picture`} alt="Profile" className={classes['profile-picture']} />
                    <div className='ms-2'>{userData.firstName} {userData.lastName}</div>
                    <button onClick={handleLogOut} className='ms-3'><FontAwesomeIcon icon={faRightFromBracket} className={`${classes.icon} ${classes["log-out-button"]}`} /></button>
                </div>
            </header>
            <nav className={classes.nav}>
                <Link to="/home" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faHouse} className={classes.icon} />
                    </div>
                    <div className='ms-2'>
                        Home
                    </div>
                </Link>
                <Link to="/chat" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faRocketchat} className={classes.icon} />
                    </div>
                    <div className='ms-2'>
                        Chat
                    </div>
                </Link>
                <Link to="/users" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faUser} className={classes.icon} />
                    </div>
                    <div className='ms-2'>
                        Users
                    </div>
                </Link>
            </nav>
            <main className={classes.content}>
                <Outlet />
            </main>
        </div>
    );
};

export default Layout;
