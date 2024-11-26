import { Col, Container, Row } from "react-bootstrap";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { useEffect, useState } from "react";
import WaitingRoom from "./components/WaitingRoom";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

function App() {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const joinChatRoom = async (
    username: string,
    chatRoom: string
  ): Promise<void> => {
    try {
      // initiate a connection
      const connection = new HubConnectionBuilder()
        // https://localhost:44347/
        // https://localhost:44347/
        .withUrl("https://localhost:44347/chat")
        .configureLogging(LogLevel.Information)
        .build();

      // set up handler
      connection.on(
        "JoinSpecificChatRoom",
        (username: string, message: string) => {
          console.log(`Hello ${username} => ${message}`);
        }
      );
      await connection.start();
      await connection.invoke("JoinSpecificChatRoom", { username, chatRoom });
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  // Clean up the SignalR connection when the component unmounts
  // useEffect(
  //   () => () => {
  //     if (connection) {
  //       connection.stop().then(() => console.log("connection stopped"));
  //     }
  //   },
  //   [connection]
  // );

  return (
    <>
      <div>
        <main>
          <Container>
            <Row className="px-5 py-5">
              <Col sm="12">
                <h1 className="text-center font-weight-light">
                  Welcome to the F1 ChatApp
                </h1>
                <WaitingRoom joinChatRoom={joinChatRoom} />
              </Col>
            </Row>
          </Container>
        </main>
      </div>
    </>
  );
}

export default App;
