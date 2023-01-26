import React, { useState, useMemo } from "react";
import { useEffect } from "react";

import * as signalR from "@microsoft/signalr";

const API_URL = "https://localhost:7289";

export const AlertContext = React.createContext();

export const AlertContextProvider = (props) => {
  const [alerts, setAlerts] = useState([]);
  const [visibility, setVisibility] = useState({});
  const [badgeNumber, setBadgeNumber] = useState(0);

  const { children } = props;

  const api =  {
      alerts,
      visibility,
      badgeNumber,
      hide: (id) => {
        setVisibility((x) => ({
          ...x,
          [id]: false,
        }));
      },
      resetBadgeNumber: () => {
        setBadgeNumber((prevcount) => 0);
      }
  }

  useEffect(() => {
    let connection = new signalR.HubConnectionBuilder()
      .withUrl(`${API_URL}/monitor-updates`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    connection.start();

    connection.on("new-alert", (data) => {
      setAlerts((prevSate) => [
        ...prevSate,
        {
          ...data,
        },
      ]);

      if(window.location.pathname !== '/alerts'){
        setBadgeNumber((prevcount) => prevcount + 1);
      }
    });
  }, []);

  return <AlertContext.Provider value={api}>{children}</AlertContext.Provider>;
};