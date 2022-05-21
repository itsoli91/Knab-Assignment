import React, { useState, useEffect } from 'react';
import { DisplayScreen } from './DisplayScreen';
import axios from 'axios';
import { useAuth } from "react-oidc-context";

const BASE_URL = 'http://localhost:1900/api';

const mapToPrice = (code, currentRate) => {
    return {
        code: code,
        currentRate: currentRate
    };
};

function BitcoinMonitor() {


    const [prices, setPrices] = useState({});
    const [ready, setReady] = useState(false);
    const [currencies, setCurrencies] = useState([]);
    const auth = useAuth();





    useEffect(() => {
        loadBitcoinPriceIndex()
    }, [])



    const callApi = () => {

        return new Promise((resolve, reject) => {
            axios.get(`${BASE_URL}/v1/Crypto/Prices/Latest?symbol=BTC`, {
                headers: {
                    'Authorization': `Bearer ${auth.user?.access_token}`
                }
            })
                .then(response => {
                    if (response.data) {
                        resolve({
                            USD: response.data.usd,
                            EUR: response.data.eur,
                            BRL: response.data.brl,
                            GBP: response.data.gbp,
                            AUD: response.data.aud
                        });
                    } else {
                        reject('No prices available');
                    }
                });
        });
    }

    const loadBitcoinPriceIndex = async () => {

        var prices = await callApi();

        if (prices) {
            setPrices({
                USD: mapToPrice('USD', prices.USD),
                EUR: mapToPrice('EUR', prices.EUR),
                BRL: mapToPrice('BRL', prices.BRL),
                GBP: mapToPrice('GBP', prices.GBP),
                AUD: mapToPrice('AUD', prices.AUD)
            });
            setReady(true);

        }

    }


    return (
        ready === true && <div>


            <div className=" mt-5">
                <button className="btn btn-lg btn-success " onClick={loadBitcoinPriceIndex}>
                    <i className="fa fa-refresh fa-lg"></i> Refresh prices
                </button>
            </div>
            <div className="h6 mt-2 ml-2 text-secondary">BTC lates quetes</div>

            <DisplayScreen
                currencies={currencies}
                prices={prices}
            />


        </div>
    );
}










export default BitcoinMonitor;