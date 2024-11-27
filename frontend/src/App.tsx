import { Col, Container, Row } from "react-bootstrap";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { useRef, useState } from "react";
import WaitingRoom from "./components/WaitingRoom";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import ChatRoom from "./components/ChatRoom";

function App() {
  const [messages, setMessages] = useState<{ msg: string; username: string }[]>(
    []
  );
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const connectionRef = useRef<HubConnection | null>(null);
  const joinChatRoom = async (
    username: string,
    chatRoom: string
  ): Promise<void> => {
    try {
      // initiate a connection
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:44347/chat")
        .configureLogging(LogLevel.Information)
        .build();

      // set up handler
      connection.on(
        "JoinSpecificChatRoom",
        (username: string, message: string) => {
          setMessages((prev) => [
            ...prev,
            { username: username, msg: message },
          ]);
        }
      );
      connection.on(
        "RecieveSpacificMessage",
        (username: string, message: string) => {
          setMessages((prev) => [
            ...prev,
            { username: username, msg: message },
          ]);
        }
      );
      await connection.start();
      await connection.invoke("JoinSpecificChatRoom", { username, chatRoom });
      connectionRef.current = connection;
    } catch (e) {
      console.error(e);
      setErrorMessage(
        "Failed to connect to the chat server. Please try again."
      );
    }
  };
  const sendMessage = async (messages: string): Promise<void> => {
    try {
      await connectionRef.current?.invoke("SendMessage", messages);
    } catch (e) {
      console.error(e);
      setErrorMessage("Failed to send the message. Please try again.");
    }
  };
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
                {errorMessage && <p className="text-danger">{errorMessage}</p>}
                {!connectionRef.current ? (
                  <WaitingRoom joinChatRoom={joinChatRoom} />
                ) : (
                  <ChatRoom
                    messages={messages}
                    sendMessage={sendMessage}
                  ></ChatRoom>
                )}
              </Col>
            </Row>
          </Container>
        </main>
      </div>
    </>
  );
}

export default App;
