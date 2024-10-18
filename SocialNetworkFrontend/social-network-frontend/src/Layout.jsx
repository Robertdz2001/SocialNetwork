import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import classes from './Layout.module.scss';

const Layout = () => {
    return (
        <div className={classes.container}>
            <header className={classes.header}>
                <h1>HEADER</h1>
            </header>
            <nav className={classes.nav}>
                <Link to="/home" className={classes["nav-link"]}>Home</Link>
                <Link to="/chat" className={classes["nav-link"]}>Chat</Link>
            </nav>
            <main className={classes.content}>
                <Outlet />
            </main>
        </div>
    );
};

export default Layout;
