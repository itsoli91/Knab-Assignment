import React from 'react';
import PropTypes from 'prop-types';
import { Indicator } from './Indicator';
import { Table, TableBody, TableCell, TableHead, TableRow, Card, Toolbar, FormControl, Select } from '@material-ui/core';

const formatNumber = (number) => number.toFixed(4).toString().padEnd(11, '0');

const DisplayScreen = (props) => {
    return (

        <div>
            <Card>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>Currency</TableCell>
                            <TableCell>Current Rate</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <TableRow>
                            <TableCell>
                                <Toolbar>
                                    <i className="fa fa-usd fa-lg"></i>
                                </Toolbar>
                            </TableCell>
                            <TableCell>
                                {formatNumber(props.prices.USD.currentRate)}
                            </TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <Toolbar>
                                    <i className="fa fa-eur fa-lg"></i>
                                </Toolbar>
                            </TableCell>
                            <TableCell>
                                {formatNumber(props.prices.EUR.currentRate)}
                            </TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <Toolbar>
                                    <i className="fa fa-gbp fa-lg"></i>
                                </Toolbar>
                            </TableCell>
                            <TableCell>
                                {formatNumber(props.prices.GBP.currentRate)}

                            </TableCell>
                        </TableRow>

                        <TableRow>
                            <TableCell>
                                <Toolbar>
                                    BRL
                                </Toolbar>
                            </TableCell>
                            <TableCell>
                                {formatNumber(props.prices.BRL.currentRate)}

                            </TableCell>
                        </TableRow>

                        <TableRow>
                            <TableCell>
                                <Toolbar>
                                    AUD
                                </Toolbar>
                            </TableCell>
                            <TableCell>
                                {formatNumber(props.prices.AUD.currentRate)}

                            </TableCell>
                        </TableRow>

                    </TableBody>
                </Table>
            </Card>
        </div>
    );
};

DisplayScreen.propTypes = {
    currencies: PropTypes.array,
    prices: PropTypes.object,
    onCurrencyChanged: PropTypes.func,
};

export { DisplayScreen };