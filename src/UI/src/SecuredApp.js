import React from 'react';
import App from './App';
import { AuthProvider } from "react-oidc-context";

import { useAuth } from "react-oidc-context";
import { Card } from '@material-ui/core';

const oidcConfig = {
    authority: "http://localhost:1901",
    client_id: "knab-spa",
    redirect_uri: "http://localhost:3000",
    onSigninCallback: () => {
        window.history.replaceState(
            {},
            document.title,
            window.location.pathname
        )
    }
};

function SecuredApp() {
    const authService = useAuth();


    const login = async () => authService.signinRedirect();
    const logout = async () => authService.removeUser();

    if (authService.isLoading) {
        return <Card>

            <div className=" mt-5 text-center">
                <div className="h6 mt-2 ml-2 text-secondary">Loading...</div>

                <button className="btn btn-lg btn-warning" onClick={() => { logout(); login(); }}>
                    <i className="fa fa-sign-in fa-lg"></i>Reset
                </button>
            </div>

        </Card>

    }

    if (!authService.isAuthenticated) {
        return (
            <Card>

                <div className=" mt-5 text-center">
                    <div className="h6 mt-2 ml-2 text-secondary">Not Logged in yet</div>

                    <button className="mb-5 btn btn-lg btn-success" onClick={login}>
                        <i className="fa fa-sign-in fa-lg"></i> Login
                    </button>
                </div>

            </Card>
        )
    }

    //const token = authService.getAuthTokens();
    return (
        <div>
            <App />

        </div>
    );
}

function WrappedSecuredApp() {
    return (
        <AuthProvider {...oidcConfig} >
            <SecuredApp />
        </AuthProvider>
    );
}

export default WrappedSecuredApp; 