import React from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import classes from './Layout.module.scss';
import icons from './Shared/icons.module.scss'
import { baseUrl, authorization } from './Shared/Options/ApiOptions';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { faGear, faHouse, faPlus, faRightFromBracket, faUser, faUserGroup } from '@fortawesome/free-solid-svg-icons';
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

    const handleEditButton = () => {
        navigate('/edit');
    }

    if (!userData) {
        return <div>Loading...</div>;
    }

    return (
        <div className={classes.container}>
            <header className={`${classes.header} d-flex align-items-center justify-content-between`}>
                <div className='m-0'>Social Network</div>
                <div className='m-0 d-flex g-2 align-item-center'>
                    <button onClick={handleEditButton} className='ms-3'><FontAwesomeIcon icon={faGear} className={`${icons.icon} ${icons["log-out-button"]} me-2`} /></button>
                    <img src={`${baseUrl}/user/${userData.userId}/profile-picture`} alt="Profile" className={classes['profile-picture']} />
                    <div className='ms-2'>{userData.firstName} {userData.lastName}</div>
                    <button onClick={handleLogOut} className='ms-3'><FontAwesomeIcon icon={faRightFromBracket} className={`${icons.icon} ${icons["log-out-button"]}`} /></button>
                </div>
            </header>
            <nav className={classes.nav}>
                <Link to="/home" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faHouse} className={icons.icon} />
                    </div>
                    <div className='ms-2'>
                        Home
                    </div>
                </Link>
                <Link to="/chat" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faRocketchat} className={icons.icon} />
                    </div>
                    <div className='ms-2'>
                        Chat
                    </div>
                </Link>
                <Link to="/users" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faUser} className={icons.icon} />
                    </div>
                    <div className='ms-2'>
                        Users
                    </div>
                </Link>
                <Link to="/friend-invites" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faPlus} className={icons.icon} />
                    </div>
                    <div className='ms-2'>
                        Friend Invites
                    </div>
                </Link>
                <Link to="/mutual-friends" className={`${classes["nav-link"]} d-flex align-item-center`}>
                    <div>
                        <FontAwesomeIcon icon={faUserGroup} className={icons.icon} />
                    </div>
                    <div className='ms-2'>
                        Mutual Friends
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
