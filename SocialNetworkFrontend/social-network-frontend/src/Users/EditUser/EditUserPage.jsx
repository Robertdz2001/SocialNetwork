import { useState, useEffect, useRef } from "react";
import axios from "axios";
import authClasses from "../../Authentication/AuthenticationPage.module.scss";
import { baseUrl, authorization } from "../../Shared/Options/ApiOptions";


function EditUserComponent() {
    const [formData, setFormData] = useState({
        firstName: "",
        lastName: "",
        phoneNumber: "",
        country: "",
        city: "",
    });
    const [profilePicture, setProfilePicture] = useState(null);
    const [previewPicture, setPreviewPicture] = useState(null);
    const firstNameErrRef = useRef(null);
    const lastNameErrRef = useRef(null);
    const phoneErrRef = useRef(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await axios.get(
                    `${baseUrl}/user/get-my-user`,
                    authorization(localStorage.getItem("token"))
                );
                const user = response.data;
                setFormData({
                    firstName: user.firstName,
                    lastName: user.lastName,
                    phoneNumber: user.phoneNumber || "",
                    country: user.country || "",
                    city: user.city || "",
                });
                setPreviewPicture(`${baseUrl}/user/${user.id}/profile-picture`);
            } catch (err) {
                console.error("Failed to fetch user data", err);
            }
        };
        fetchUserData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handlePictureChange = (e) => {
        const file = e.target.files[0];
        if (file && (file.type === "image/jpeg" || file.type === "image/png" || file.type === "image/jpg")) {
            setProfilePicture(file);
            setPreviewPicture(URL.createObjectURL(file));
        }
    };

    const handleFormSubmit = async (e) => {
        e.preventDefault();

        const dataToSend = new FormData();
        dataToSend.append("FirstName", formData.firstName);
        dataToSend.append("LastName", formData.lastName);
        dataToSend.append("PhoneNumber", formData.phoneNumber);
        dataToSend.append("Country", formData.country);
        dataToSend.append("City", formData.city);

        if (profilePicture) {
            dataToSend.append("ProfilePicture", profilePicture);
        }

        let hasErrors = false;

        const phoneRegex = /^\+?[1-9]\d{1,14}$/;
        if (formData.phoneNumber && !phoneRegex.test(formData.phoneNumber)) {
            phoneErrRef.current.innerHTML = "Phone number is invalid. It should be in international format.";
            hasErrors = true;
        } else {
            phoneErrRef.current.innerHTML = "";
        }

        if (hasErrors) return;

        try {
            const token = localStorage.getItem("token");
            await axios.put(
                `${baseUrl}/user/update-user`,
                dataToSend,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        "Content-Type": "multipart/form-data"
                    },
                }
            );
            alert("Your details have been updated successfully.");
            window.location.reload();
        } catch (err) {
            console.error("Failed to update user details", err);
            alert("An error occurred while updating your details.");
        }
    };

    return (
        <>
            <h2 className={authClasses["auth-header"]}>Edit Profile</h2>
            <form onSubmit={handleFormSubmit} className={authClasses["auth-form"]}>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="firstName">First Name</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="text"
                        id="firstName"
                        name="firstName"
                        value={formData.firstName}
                        onChange={handleInputChange}
                        placeholder="Enter your first name"
                        maxLength={20}
                        required
                    />
                    <span ref={firstNameErrRef} className={authClasses["auth-error-message"]}></span>
                </div>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="lastName">Last Name</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="text"
                        id="lastName"
                        name="lastName"
                        value={formData.lastName}
                        onChange={handleInputChange}
                        placeholder="Enter your last name"
                        maxLength={20}
                        required
                    />
                    <span ref={lastNameErrRef} className={authClasses["auth-error-message"]}></span>
                </div>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="phoneNumber">Phone Number</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="tel"
                        id="phoneNumber"
                        name="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={handleInputChange}
                        placeholder="Enter your phone number"
                        maxLength={30}
                        required
                    />
                    <span ref={phoneErrRef} className={authClasses["auth-error-message"]}></span>
                </div>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="country">Country</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="text"
                        id="country"
                        name="country"
                        value={formData.country}
                        onChange={handleInputChange}
                        placeholder="Enter your country"
                        maxLength={30}
                        required
                    />
                </div>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="city">City</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="text"
                        id="city"
                        name="city"
                        value={formData.city}
                        onChange={handleInputChange}
                        placeholder="Enter your city"
                        maxLength={30}
                        required
                    />
                </div>
                <div className={authClasses["auth-input-group"]}>
                    <label className={authClasses["auth-label"]} htmlFor="profilePicture">Profile Picture</label>
                    <input
                        className={authClasses["auth-input"]}
                        type="file"
                        id="profilePicture"
                        accept="image/jpeg,image/png,image/jpg"
                        onChange={handlePictureChange}
                    />
                    {previewPicture && <img src={previewPicture} alt="Profile Preview" className={authClasses["profile-preview"]} />}
                </div>
                <div className="d-flex justify-content-center">
                    <button type="submit" className={`${authClasses["auth-primary-btn"]} w-25`}>Save Changes</button>
                </div>
            </form>
        </>
    );
}

export default EditUserComponent;
