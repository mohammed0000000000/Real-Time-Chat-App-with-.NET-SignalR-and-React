import { ReactNode } from "react";

interface IProps {
  messages: { username: string; msg: string }[]; // Replace with your own message type
}
const MessageContainer = ({ messages }: IProps): ReactNode => {
  return (
    <div>
      {messages.map((message, index) => (
        <table>
          <tr key={index}>
            <td>
              {message.username} - {message.msg}{" "}
            </td>
          </tr>
        </table>
      ))}
    </div>
  );
};

export default MessageContainer;
