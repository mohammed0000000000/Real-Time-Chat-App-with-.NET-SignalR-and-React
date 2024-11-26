import { ChangeEvent, FormEvent, useState } from "react";
import { Button, Form } from "react-bootstrap";

interface IProps {
  joinChatRoom: (username: string, chatRoom: string) => void;
}

const WaitingRoom = ({ joinChatRoom }: IProps) => {
  const [user, setUser] = useState<{ username: string; chatRoom: string }>({
    username: "",
    chatRoom: "",
  });
  const inputHandler = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setUser((prev) => ({ ...prev, [name]: value }));
  };
  const formHandler = async (e: FormEvent) => {
    e.preventDefault();
    await joinChatRoom(user.username, user.chatRoom);
    setUser({ username: "", chatRoom: "" });
  };
  return (
    <>
      <Form onSubmit={formHandler} className="p-3">
        <Form.Group controlId="formUsername" className="mb-3">
          <Form.Label>Username</Form.Label>
          <Form.Control
            type="text"
            placeholder="Enter your username"
            name="username"
            value={user.username}
            onChange={inputHandler}
            required
          />
        </Form.Group>
        <Form.Group controlId="formChatRoom" className="mb-3">
          <Form.Label>Chat Room</Form.Label>
          <Form.Control
            type="text"
            placeholder="Enter chat room name"
            name="chatRoom"
            value={user.chatRoom}
            onChange={inputHandler}
            required
          />
        </Form.Group>
        <Button variant="primary" type="submit">
          Join Chat Room
        </Button>
      </Form>
    </>
  );
};

export default WaitingRoom;
