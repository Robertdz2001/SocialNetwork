import React from 'react';
import classes from './GroupInviteComponent.module.scss';
import { baseUrl, authorization } from '../../Shared/Options/ApiOptions';
import fonts from '../../Shared/fonts.module.scss';
import axios from 'axios';
import { useState } from 'react';

const GroupInviteComponent = ({ groupInvite }) => {
    const [isInviteAnswered, setIsInviteAnswered] = useState(false);

    const handleAcceptGroup = async () => {
        try {
            await axios.put(
                `${baseUrl}/group/answer-invite`,
                {
                    groupId: groupInvite.id,
                    isAccepted: true
                },
                authorization(localStorage.getItem("token"))
            );
            setIsInviteAnswered(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    const handleRejectGroup = async () => {
        try {
            await axios.put(
                `${baseUrl}/group/answer-invite`,
                {
                    groupId: groupInvite.id,
                    isAccepted: false
                },
                authorization(localStorage.getItem("token"))
            );
            setIsInviteAnswered(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    return (
        <>
            {!isInviteAnswered && (
                <div key={groupInvite.id} className={`${classes["group-invite-item"]} d-flex align-items-center mb-5`}>
                    <div>
                        <img src={`${baseUrl}/group/${groupInvite.id}/photo`} alt="Profile" className={classes['group-invite-photo']} />
                    </div>
                    <div className='ms-3'>
                        <div className={fonts["font-green-medium"]}>
                            {groupInvite.name}
                        </div>
                    </div>
                    <button onClick={handleRejectGroup} className={`${classes["group-invite-button"]} ${classes["group-invite-button-reject"]}`}>Reject</button>
                    <button onClick={handleAcceptGroup} className={`${classes["group-invite-button"]} ${classes["group-invite-button-accept"]}`}>Accept</button>
                </div>
            )}
        </>
    );
};

export default GroupInviteComponent;