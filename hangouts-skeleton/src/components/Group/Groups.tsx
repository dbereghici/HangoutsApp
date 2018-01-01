import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import { AddNewGroupForm } from './AddNewGroupForm';
import { AllGroupsList } from './AllGroupsList';
import { MyGroupsList } from './MyGroupsList';

export class Groups extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.setGroupsType = this.setGroupsType.bind(this);

        this.state = {
            groupsType: "member"
        }
    }



    setGroupsType(type: string) {
        this.setState({
            groupsType: type
        }, () => this.forceUpdate())
    }

    render() {
        // if (!this.state.isAuth) {
        //     return <Redirect to='/authentication' />;
        // }

        return (
            <div>
                <Header />
                <div className="container">
                    <button type="button" className="btn btn-success" onClick={() => this.setGroupsType("member")}> My groups</button>
                    <button type="button" className="btn btn-success" onClick={() => this.setGroupsType("admin")}> Group Administrated </button>
                    <button type="button" className="btn btn-success" onClick={() => this.setGroupsType("sent")}> Group Invitations Sent </button>
                    <button type="button" className="btn btn-success" onClick={() => this.setGroupsType("received")}> Group Invitations Received </button>
                    <button type="button" className="btn btn-success" onClick={() => this.setGroupsType("all")}> All groups </button>
                    <button type="button" className="btn btn-danger" onClick={() => this.setGroupsType("addnew")}> Add New Group</button>
                    

                    {this.state.groupsType === "all" ?
                        <div>
                            <h2> All Groups </h2>
                            <AllGroupsList />
                        </div>
                        :
                        this.state.groupsType === "addnew" ?
                            <div>
                                <h2> Add New Group </h2>
                                <AddNewGroupForm />
                            </div>
                            :
                            <div>
                                <MyGroupsList status={this.state.groupsType} />
                            </div>
                    }
                </div>
  
            </div >
        );
    }
}
