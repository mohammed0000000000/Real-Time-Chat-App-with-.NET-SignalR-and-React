import { ReactNode } from "react";
import { Col, Row } from "react-bootstrap";
import MessageContainer from "./MessageContainer";
import SendMessageForm from "./SendMessageForm";
interface IProps {
  messages: { msg: string; username: string }[];
  sendMessage: (message: string) => void;
}
const ChatRoom = ({ messages, sendMessage }: IProps): ReactNode => {
  return (
    <div>
      <Row className="px-5 py-5">
        <Col sm={10}>
          <h2>Chat Room</h2>
        </Col>
        <Col></Col>
      </Row>
      <Row className="px-5 py-5">
        <Col sm={12}>
          <SendMessageForm sendMessage={sendMessage}></SendMessageForm>
          <MessageContainer messages={messages}></MessageContainer>
        </Col>
      </Row>
    </div>
  );
};
export default ChatRoom;
