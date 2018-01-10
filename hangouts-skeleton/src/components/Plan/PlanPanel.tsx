import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import { PlanService } from '../../services/PlanService';
import AuthService from '../../services/AuthService';
import { Redirect } from 'react-router';

export default class PlanPanel extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.leave = this.leave.bind(this);
        this.message = this.message.bind(this);
        this.join = this.join.bind(this);

        this.state = {
            plan: this.props.plan,
            redirectToChat: false,
            members: [{
                address: null,
                age: 0,
                firstName: '',
                id: 0,
                lastName: '',
                relationshipStatus: null,
                username: ''
            }],
            authUser: JSON.parse(AuthService.getUserData())
        }
    }

    componentDidMount() {
        if (!!this.state.plan)
            PlanService.GetUsersFromPlan(this.state.plan.id).then(
                (users) => {
                    this.setState({ members: users })
                },
                (error) => {
                    this.setState({ members: [] })
                }
            )
    }

    leave() {
        debugger;
        PlanService.DeleteUserFromPlan(this.state.plan.id, this.state.authUser.id).then(
            () => {
                PlanService.GetUsersFromPlan(this.state.plan.id).then(
                    (users) => {
                        this.setState({ members: users })
                        this.props.refresh(this.state.plan.id);
                    },
                    (error) => {
                        this.setState({ members: [] })
                        this.props.refresh(this.state.plan.id);                        
                    }
                )
            },
            (error) => {
                this.props.refresh(this.state.plan.id);
            }
        )
    }

    message() {
        this.setState({
            redirectToChat: true
        })
    }

    join() {
        PlanService.AddUserToPlan(this.state.plan.id, this.state.authUser.id).then(
            () => {
                PlanService.GetUsersFromPlan(this.state.plan.id).then(
                    (users) => {
                        this.setState({ members: users });
                        this.props.refresh(this.state.plan.id);
                    },
                    (error) => {
                        this.setState({ members: [] })
                        this.props.refresh(this.state.plan.id);
                    }
                )
            },
            (error) => {
                this.props.refresh(this.state.plan.id);

            }
        )
    }

    render() {
        if (this.state.redirectToChat) {
            let redirectTo = '/chat/plan/' + this.state.plan.id;
            return <Redirect to={redirectTo} />;
        }
        if(this.state.members === []){
            this.props.refresh(this.state.plan.id);
        }
        return (
            !!this.props.plan && !!this.props.plan.activity ?
                <div className="panel panel-danger" style={{ width: "300px" }} >
                    <div className="panel-heading">
                        {/* <img src={logo} width="50px" height="50px" /> */}

                        {/* <b>{this.state.UserData.username}</b> */}
                        <div>
                            <div>
                                <h3>
                                    <img src={require('../../images/' + this.props.plan.activity + '.png')}
                                        width="70px"
                                        height="70px"
                                        alt="Cinque Terre"
                                    />
                                    {this.props.plan.activity}
                                </h3>
                            </div>
                        </div>

                    </div>
                    <div className="panel-body">
                        <b> From </b>
                        {this.props.plan.startTime.slice(0, 10)} {this.props.plan.startTime.slice(11, this.props.plan.startTime.length)}
                        <br />
                        <b> To </b>
                        {this.props.plan.startTime.slice(0, 10)} {this.props.plan.endTime.slice(11, this.props.plan.endTime.length)}
                        <br />
                        <b> Location: </b>
                        {!!this.props.plan.address && !!this.props.plan.address.location ? this.props.plan.address.location : this.props.plan.address}
                        <br />
                        <b> Members: </b>
                        {/* {this.state.members[0].username} */}
                        {
                            this.state.members.length > 0 ?
                                this.state.members.length > 1 ?
                                    <p>
                                        {this.state.members[0].username}
                                        ,{'\u00A0'}
                                        {this.state.members[1].username}
                                        <a style={{display: "table-cell", color: "red"}} href={"../members/plan/"+this.state.plan.id} target="_blank">
                                        {this.state.members.length > 2 ? <p> and {this.state.members.length - 2} more </p> : <div />}                                        
                                        </a>
                                    </p>
                                    :
                                    this.state.members[0].username
                                :
                                <div />
                        }
                        {this.props.hideButton === false && this.state.members!==[]?
                            this.props.plan.status === "member" ?
                                <div>
                                    <button type="button" className="btn btn-warning glyphicon glyphicon-remove" onClick={this.leave}> Leave </button>
                                    <button type="button" className="btn btn-warning glyphicon glyphicon-envelope" onClick={this.message}> Message </button>
                                </div>
                                :
                                <div>
                                    <button type="button" className="btn btn-warning glyphicon glyphicon-ok" onClick={this.join}> Join </button>
                                </div>
                            : <div />
                        }
                    </div>
                </div>
                :
                <div />
        )
    }
}