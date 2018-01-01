import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import Message from './Message';
// import { GiftedChat } from 'react-native-gifted-chat';
import './Chat.css';
import { Header } from '../Header/Header';
import ChatService from '../../services/ChatService';
import AuthService from '../../services/AuthService';
import { IUser } from '../../models/IUser';
import MessageService from '../../services/MessageService';

export default class Chat extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.sendMessage = this.sendMessage.bind(this);

        this.state = {
            errorMessage: '',
            newMessage: '',
            formValid: false,
            Chat: {
                id: 0,
                messages: [

                ],
                users: [

                ]
            }
        }
    }

    componentDidMount() {
        this.props.match.params.type === "user" ?
            //"user"            
            ChatService.getChatOfFriendship(this.props.match.params.id, JSON.parse(AuthService.getUserData()).id).then(
                (Chat) => {
                    this.setState({
                        Chat: Chat
                    })
                },
                (error) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
            :
            //"plan"
            ChatService.getChatOfPlan(this.props.match.params.id).then(
                (Chat) => {
                    this.setState({
                        Chat: Chat
                    })
                },
                (error) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
    }

    renderMessage(message: any, index: number): JSX.Element {
        return (
            <Message message={message} />
        )
    }

    renderMembers(member: IUser, index: number): JSX.Element {
        return (
            <p>
                <b> {member.username} </b>
            </p>
        )
    }

    handleUserInput(e: any) {
        this.setState({ newMessage: e.target.value })
        if (e.target.value === "" || e.target.value.length > 200)
            this.setState({ formValid: false })
        else
            this.setState({ formValid: true })
    }


    sendMessage(event: any) {
        event.preventDefault();
        let message = {
            chatid: this.state.Chat.id,
            content: this.state.newMessage,
            createdAt: Date,
            userID: JSON.parse(AuthService.getUserData()).id,
        }
        MessageService.addMessage(message).then(
            () =>
                this.props.match.params.type === "user" ?
                    //"user"            
                    ChatService.getChatOfFriendship(this.props.match.params.id, JSON.parse(AuthService.getUserData()).id).then(
                        (Chat) => {
                            this.setState({
                                Chat: Chat
                            });
                        },
                        (error) => {
                            if (error && error.response && error.response.data)
                                this.setState({ errorMessage: error.response.data })
                            else if (error.message)
                                this.setState({ errorMessage: error.message })
                        }
                    )
                    :
                    //"plan"
                    ChatService.getChatOfPlan(this.props.match.params.id).then(
                        (Chat) => {
                            this.setState({
                                Chat: Chat
                            })
                        },
                        (error) => {
                            if (error && error.response && error.response.data)
                                this.setState({ errorMessage: error.response.data })
                            else if (error.message)
                                this.setState({ errorMessage: error.message })
                        }
                    )
        )
    }


    render() {
        return (
            <div>
                <Header />
                <div className="row">
                    <div className="col-sm-3">
                        <p> {this.state.errorMessage} </p>
                        <b> Participants: </b>
                        {typeof this.state.Chat.users === "undefined" ? <b>"There are no users in this chat"</b> : this.state.Chat.users.map(this.renderMembers)}
                        <br />
                    </div>
                    <div className="col-sm-9">
                        <div className="scroll">
                            {typeof this.state.Chat.users === "undefined" ? "There are no messages in this chat" : this.state.Chat.messages.map(this.renderMessage)}
                        </div>
                        <form className="demoForm" onSubmit={this.sendMessage}>
                            <div className="form-group">
                                <input type="text" className="form-control"
                                    name="text" onChange={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            <button type="submit" className="btn btn-primary"
                                disabled={!this.state.formValid}>Send</button>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}