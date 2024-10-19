import React from 'react';
import classes from './UserComponent.module.scss';
import { baseUrl } from '../../../Shared/Options/ApiOptions';

const UserComponent = ({ user }) => {
    return (
        <div key={user.id} className={`${classes["user-item"]} d-flex align-items-center`}>
            <div>
                <img src={`${baseUrl}/user/${user.userId}/profile-picture`} alt="Profile" className={classes['user-profile-picture']} />
            </div>
            <div className='ms-3'>
                {user.firstName} {user.lastName} - {user.city == null ? "No city" : user.city}, {user.country == null ? "No country" : user.country}
            </div>
        </div>
    );
};

export default UserComponent;