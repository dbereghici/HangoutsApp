import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import { PlanService } from '../../services/PlanService';
import PlanPanel from './PlanPanel';
import { Redirect } from 'react-router';

export default class NewPlan extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.refresh = this.refresh.bind(this);

        this.state = {
            activity : this.props.activity,
            groupId : this.props.groupId,
            startTime : this.props.startTime,
            endTime : this.props.endTime,
            address : this.props.address,
            errorMessage: '',
            homeRedirect: false, 
            plan : {
                id: 0, 
                activity: '', 
                activityId: 0, 
                adress: {
                    id: 0, 
                    latitude: 0,
                    location: '',
                    longitude: 0
                },
                chatId: 0, 
                endTime: new Date(),
                groupId: 0, 
                startTime: new Date(),
                status: ''
            }
        }
    }

    componentDidMount(){
        PlanService.AddNewPlan(this.state.groupId, this.state.activity, this.state.startTime, this.state.endTime, this.state.address).then(
            (plan: any) => {
                this.setState({
                    plan: plan, 
                    errorMessage: ''
                })
            }, 
            (error) => {
                if (error && error.response && error.response.data){
                    this.setState({errorMessage: error.response.data})
                }
                else if (error.message)
                    this.setState({errorMessage: error.message})
            }
        );
    }

    refresh(id: number){
        debugger;
        PlanService.GetPlanById(id).then(
            (plan: any) => {
                debugger;
                this.setState({
                    plan: plan, 
                    errorMessage: ''
                })
            }, 
            (error) => {
                if (error && error.response && error.response.data){
                    this.setState({errorMessage: error.response.data})
                }
                else if (error.message)
                    this.setState({errorMessage: error.message})
            }
        );
        alert("The plan was removed");
        this.setState({homeRedirect: true});

    }

    render() {
        if(this.state.homeRedirect){
            let redirectTo = '/home/';
            return <Redirect to={redirectTo} />;
        }
        return (
            <div>
                <p> {this.state.errorMessage} </p>
                <PlanPanel plan = {this.state.plan} refresh = {this.refresh} hideButton={true}/>
            </div>
        )
    }
} 