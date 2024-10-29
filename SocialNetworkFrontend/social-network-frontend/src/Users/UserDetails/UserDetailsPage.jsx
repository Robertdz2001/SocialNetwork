import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl, authorization } from "../../Shared/Options/ApiOptions.js";
import classes from './UserDetailsPage.module.scss';
import fonts from '../../Shared/fonts.module.scss';
import icons from '../../Shared/icons.module.scss';
import { faPhone } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

function UserDetailsPage() {
    const { id } = useParams();
    const [isFriend, setIsFriend] = useState(null);
    const [isInvited, setIsInvited] = useState(null);
    const [userData, setUserData] = useState(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await axios.get(`${baseUrl}/user/${id}`, authorization(localStorage.getItem("token")));
                setUserData(response.data);
                setIsFriend(response.data.isFriend);
                setIsInvited(response.data.isInvited);
            } catch (error) {
                console.error("Error fetching user data:", error);
            }
        };
        fetchUserData();
    }, [id]);

    const handleAddFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/${userData.id}/add-friend`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsInvited(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    const handleDeleteFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/${userData.id}/delete-friend`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsFriend(false);
        } catch (err) {
        }
    }

    return (
        <div>
            {userData ? (
                <div className='d-flex justify-content-center'>
                    <div className={`${classes['user-details-container']} d-flex`}>
                        <img
                            src={`${baseUrl}/user/${userData.id}/profile-picture`}
                            alt="Profile"
                            className={classes['user-details-profile-picture']} />
                        <div className={`${fonts["font-green-large"]} ${classes['user-details-border']} d-flex align-items-center ms-4`}>
                            {userData.firstName} {userData.lastName}
                        </div>
                        <div className={`${fonts["font-grey-medium"]} d-flex justify-content-center flex-column ms-4`}>
                            <p className={`${classes['user-details-margin']}`}>{userData.country}</p>
                            <p className={`${classes['user-details-margin']}`}>{userData.city}</p>
                            <div className='d-flex align-items-center'>
                                <FontAwesomeIcon icon={faPhone} className={icons.icon} />
                                <p className="ms-2 m-0">{userData.phoneNumber}</p>
                            </div>
                        </div>
                        {isFriend && (
                            <button onClick={handleDeleteFriend} className={`${classes["user-button"]} ${classes["user-button-delete"]}`}>Delete</button>
                        )}
                        {isInvited && (
                            <button className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Invited</button>
                        )}
                        {!isInvited && !isFriend && (
                            <button onClick={handleAddFriend} className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Add</button>
                        )}
                    </div>
                </div>
            ) : (
                <p>Loading user data...</p>
            )}
        </div>
    );
}

export default UserDetailsPage;
