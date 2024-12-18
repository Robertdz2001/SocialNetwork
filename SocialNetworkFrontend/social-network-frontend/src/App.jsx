import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./Layout";
import HomePage from "./Home/HomePage";
import ChatPage from "./Chat/ChatPage";
import UsersPage from "./Users/UsersPage";
import './App.module.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import AuthenticationPage from "./Authentication/AuthenticationPage";
import { useState, useEffect } from "react";
import axios from "axios";
import { baseUrl, authorization } from "./Shared/Options/ApiOptions";
import FriendInvitesPage from "./FriendInvites/FriendInvitesPage";
import MutualFriendsPage from "./MutualFriends/MutualFriendsPage";
import UserDetailsPage from "./Users/UserDetails/UserDetailsPage";
import EditUserPage from "./Users/EditUser/EditUserPage";
import GroupsPage from "./Groups/GroupsPage";
import GroupInvitesPage from "./GroupInvites/GroupInvitesPage";
import GroupDetailsPage from "./Groups/GroupDetails/GroupDetailsPage";

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(null);

  useEffect(() => {
    const checkIfUserIsLoggedIn = async () => {
      if (localStorage.getItem("token")) {
        try {
          await axios.get(
            `${baseUrl}/user/is-logged-in`,
            authorization(localStorage.getItem("token"))
          );

          setIsAuthenticated(true);

        } catch (err) {
          setIsAuthenticated(false);
        }
      } else {
        setIsAuthenticated(false);
      }
    };
    checkIfUserIsLoggedIn();
  }, []);

  if (isAuthenticated === null) {
    return <div>Loading...</div>;
  }

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            isAuthenticated ? (
              <Navigate to="/home" replace={true} />
            ) : (
              <Navigate to="/auth" replace={true} />
            )
          }
        />
        <Route element={<Layout isAuthenticated={isAuthenticated} />}>
          <Route
            path="/home"
            element={isAuthenticated ? <HomePage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/chat"
            element={isAuthenticated ? <ChatPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/users"
            element={isAuthenticated ? <UsersPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/friend-invites"
            element={isAuthenticated ? <FriendInvitesPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/mutual-friends"
            element={isAuthenticated ? <MutualFriendsPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/users/:id"
            element={isAuthenticated ? <UserDetailsPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/edit"
            element={isAuthenticated ? <EditUserPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/groups"
            element={isAuthenticated ? <GroupsPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/group-invites"
            element={isAuthenticated ? <GroupInvitesPage /> : <Navigate to="/auth" replace />}
          />
          <Route
            path="/groups/:id"
            element={isAuthenticated ? <GroupDetailsPage /> : <Navigate to="/auth" replace />}
          />
        </Route>

        <Route path="/auth" element={!isAuthenticated ? <AuthenticationPage /> : <Navigate to="/" replace />} />

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App
