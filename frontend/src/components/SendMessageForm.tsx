import { FormEvent, ReactNode, useState } from "react";
import { Button, Form, InputGroup } from "react-bootstrap";

interface IProps {
  sendMessage: (msg: string) => void; // Replace with your own message type  // Add additional props if needed  // Example: sendMessage: (msg: string, username: string) => void;  // Add additional props if needed  // Example: sendMessage: (msg: string, username: string, timestamp: Date) => void;  // Add additional props if needed   // Example: sendMessage: (msg: string, username: string, timestamp: Date, chatRoom: string) => void;  // Add additional props if needed  // Example: sendMessage: (msg: string, username: string, timestamp: Date, chatRoom: string, color: string) => void;  // Add additional props if needed   // Example: sendMessage: (msg: string, username: string, timestamp: Date, chatRoom: string, color: string, font: string) => void;  // Add additional props if needed   // Example: sendMessage:
}
const SendMessageForm = ({ sendMessage }: IProps): ReactNode => {
  const [message, setMessage] = useState("");
  const submitHandler = (e: FormEvent) => {
    e.preventDefault();
    sendMessage(message);
    setMessage("");
  };
  return (
    <Form onSubmit={submitHandler}>
      <InputGroup className="mb-3">
        <InputGroup.Text>Chat</InputGroup.Text>
        <Form.Control
          onChange={(e) => {
            setMessage(e.target.value);
          }}
          value={message}
          placeholder="Enter your Message"
        ></Form.Control>
        <Button
          variant="primary"
          type="submit"
          disabled={message ? false : true}
        >
          Send
        </Button>
      </InputGroup>
    </Form>
  );
};

export default SendMessageForm;
