import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import { SendReqList } from './SendReqList';
import { ReceivedReqList } from './ReceivedReqList';
import { AcceptedReqList } from './AcceptedReqList';
import { SearchFriendList } from './SearchFriendList';

export class Friends extends BaseComponent {
    constructor(props: any) {
        super(props);
    
    }


    render() {
        if (!this.state.isAuth) {
            return <Redirect to='/authentication' />;
        }

        return (
            <div>
                <Header />
                <div className="container">
                    <ul className="nav nav-tabs">
                        <li className="active"><a data-toggle="tab" href="#home">All friends</a></li>
                        <li><a data-toggle="tab" href="#menu1">Friend Requests Sent</a></li>
                        <li><a data-toggle="tab" href="#menu2">Friend Requests Received</a></li>
                        <li><a data-toggle="tab" href="#menu3">Search</a></li>
                        
                    </ul>

                    <div className="tab-content">
                        <div id="home" className="tab-pane fade in active">
                            <AcceptedReqList />                     
                        </div>
                        <div id="menu1" className="tab-pane fade">
                            <SendReqList/> 
                        </div>
                        <div id="menu2" className="tab-pane fade">
                            <ReceivedReqList /> 
                        </div>
                        <div id="menu3" className="tab-pane fade">
                            <SearchFriendList /> 
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
