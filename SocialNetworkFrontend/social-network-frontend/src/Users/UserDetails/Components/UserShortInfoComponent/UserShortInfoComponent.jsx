import React from 'react';
import classes from './UserShortInfoComponent.module.scss';
import { baseUrl } from '../../../../Shared/Options/ApiOptions.js';
import fonts from '../../../../Shared/fonts.module.scss';
import { useNavigate } from 'react-router-dom';

const UserShortInfoComponent = ({ user }) => {
    const navigate = useNavigate();

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${user.id}`);
    };

    return (
        <div key={user.id} className={`${classes["user-item"]} d-flex align-items-center mb-5`}>
            <div>
                <img
                    src={`${baseUrl}/user/${user.id}/profile-picture`}
                    alt="Profile"
                    className={classes['user-profile-picture']}
                    onClick={handleNavigateToUserDetails} />
            </div>
            <div className='ms-3'>
                <div className={fonts["font-green-small"]}>
                    {user.firstName} {user.lastName}
                </div>
            </div>
        </div>
    );
};

export default UserShortInfoComponent;