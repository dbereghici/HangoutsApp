import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
// import { GiftedChat } from 'react-native-gifted-chat';
import './Chat.css';

export default class Message extends BaseComponent {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <div className="msgcontainer">
                <p><b> {this.props.message.user}  </b></p>
                <p>{this.props.message.content}</p>
                <span className="time-right">{this.props.message.createdAt.slice(0,10)} {this.props.message.createdAt.slice(11,19)}</span>
            </div>
        );
    }
}