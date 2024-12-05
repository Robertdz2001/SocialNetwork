import React from 'react';
import classes from './MemberShortInfoComponent.module.scss';
import { baseUrl, authorization } from '../../../../Shared/Options/ApiOptions.js';
import fonts from '../../../../Shared/fonts.module.scss';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const MemberShortInfoComponent = ({ member, groupId, handleRefresh }) => {
    const navigate = useNavigate();

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${member.id}`);
    };

    const handleDeleteMember = async () => {
        if (!window.confirm("Are you sure you want to delete this member?")) {
            return;
        }

        try {
            await axios.delete(
                `${baseUrl}/group/${groupId}/delete-member/${member.id}`,
                authorization(localStorage.getItem("token"))
            );
            handleRefresh();
        } catch (error) {
            alert(error.response.data);
        }
    }

    return (
        <div key={member.id} className={`${classes["member-item"]} d-flex align-items-center mb-5`}>
            <div>
                <img
                    src={`${baseUrl}/user/${member.id}/profile-picture`}
                    alt="Profile"
                    className={classes['member-profile-picture']}
                    onClick={handleNavigateToUserDetails} />
            </div>
            <div className='ms-3'>
                <div className={fonts["font-green-small"]}>
                    {member.firstName} {member.lastName}
                </div>
            </div>
            <div className='ms-3'>
                {member.canDelete && (
                    <button onClick={handleDeleteMember} className={`${classes["member-button"]} ${classes["member-button-delete"]}`}>Delete</button>
                )}
            </div>
        </div>
    );
};

export default MemberShortInfoComponent;