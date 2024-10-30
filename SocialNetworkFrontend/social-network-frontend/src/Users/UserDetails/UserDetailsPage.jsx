import { useParams } from "react-router-dom";
import { useEffect, useState, useRef } from "react";
import axios from "axios";
import { baseUrl, authorization } from "../../Shared/Options/ApiOptions.js";
import classes from './UserDetailsPage.module.scss';
import fonts from '../../Shared/fonts.module.scss';
import icons from '../../Shared/icons.module.scss';
import { faPager, faPhone, faUser, faUserGroup } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import UserShortInfoComponent from "./Components/UserShortInfoComponent/UserShortInfoComponent.jsx";

function UserDetailsPage() {
    const { id } = useParams();
    const [isFriend, setIsFriend] = useState(null);
    const [isInvited, setIsInvited] = useState(null);
    const [chosenNav, setChosenNav] = useState('Users');
    const [userData, setUserData] = useState(null);
    const userNavRef = useRef(null);
    const postNavRef = useRef(null);

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

    const handleUserNav = () => {
        userNavRef.current.classList.add(classes['user-details-nav-button-active']);
        postNavRef.current.classList.remove(classes['user-details-nav-button-active']);
        setChosenNav('Users');
    }

    const handlePostNav = () => {
        userNavRef.current.classList.remove(classes['user-details-nav-button-active']);
        postNavRef.current.classList.add(classes['user-details-nav-button-active']);
        setChosenNav('Posts');
    }

    return (
        <div>
            {userData ? (
                <div className='d-flex align-items-center flex-column'>
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
                        {!userData.isItMyUser && (
                            <>
                                {isFriend && (
                                    <button onClick={handleDeleteFriend} className={`${classes["user-button"]} ${classes["user-button-delete"]}`}>Delete</button>
                                )}
                                {isInvited && (
                                    <button className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Invited</button>
                                )}
                                {!isInvited && !isFriend && (
                                    <button onClick={handleAddFriend} className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Add</button>
                                )}
                            </>
                        )}
                    </div>
                    <div className="mt-3 d-flex">
                        <div>
                            <button ref={userNavRef} onClick={handleUserNav} className={`${classes['user-details-nav-button']} ${classes['user-details-nav-button-active']}`}><FontAwesomeIcon icon={faUser} className={icons.icon} /> {userData.friends.length}</button>
                        </div>
                        <div className="ms-4">
                            <button ref={postNavRef} onClick={handlePostNav} className={classes['user-details-nav-button']}><FontAwesomeIcon icon={faPager} className={icons.icon} /> {userData.friends.length}</button>
                        </div>
                    </div>
                    {chosenNav === 'Users' ? (
                        <div className={`${classes['friends-container']} mt-4`}>
                            {userData.friends.map(friend => (
                                <UserShortInfoComponent key={friend.id} user={friend} />
                            ))}
                        </div>
                    ) : (
                        <div className="mt-4">
                            Posts
                        </div>
                    )}
                </div>
            ) : (
                <p>Loading user data...</p>
            )}
        </div>
    );
}

export default UserDetailsPage;
