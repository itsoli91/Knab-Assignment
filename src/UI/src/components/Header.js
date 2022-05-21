import React from 'react';
import PropTypes from 'prop-types';
import { AppBar, Toolbar, Typography, Box } from '@material-ui/core';
import { useAuth } from "react-oidc-context";

function Header(props) {
    const authService = useAuth();

    const logout = async () => authService.removeUser();
    return (

        <AppBar position="static">
            <div className="text-center mx-auto">
                <Toolbar>

                    <Typography align="center" variant="h6" component="div">
                        <Box fontWeight="fontWeightBold" fontSize={26} fontStyle="italic" fontFamily="Monospace" m={1}>
                            {props.title}
                        </Box>
                    </Typography>

                    <button className="btn btn-danger" onClick={logout}>
                        <i className="fa fa-sign-out fa-lg"></i>Logout
                    </button>
                </Toolbar>
            </div>
        </AppBar>
    );
}

Header.defaultProps = {
    title: 'App'
};

Header.propTypes = {
    title: PropTypes.string
};

export default Header;