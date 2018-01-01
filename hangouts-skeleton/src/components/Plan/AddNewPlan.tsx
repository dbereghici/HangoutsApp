import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import SelectActivity from './SelectActivity';
import { Header } from '../Header/Header';
import { AddressSelector } from '../AddressSelector/AddressSelector';
import PlanPanel from './PlanPanel';
import { PlanService } from '../../services/PlanService';
import AuthService from '../../services/AuthService';
import { Redirect } from 'react-router';
import NewPlan from './NewPlan';
import SimilarPlansList from './SimilarPlansList';

export default class AddNewPlan extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.setSelectedActivity = this.setSelectedActivity.bind(this);
        this.back = this.back.bind(this);
        this.addPlan = this.addPlan.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);
        this.homeRedirect = this.homeRedirect.bind(this);
        this.createNewPlan = this.createNewPlan.bind(this);

        this.state = {
            selectedActivity: Infinity,
            activityDescription: "",
            formValid: false,
            startTime: new Date(),
            endTime: new Date(),
            inputData: true,
            errorMessage: '',
            homeRedirect: false,
            address: {
                location: '',
                latitude: 0,
                longitude: 0
            },
            similarPlans: true
        }
    }

    setSimilarPlans() {
        // PlanService.GetSimilarPlansPage(1, 6, this.state.startTime, this.state.endTime, JSON.parse(AuthService.getUserData()).id, 1, this.state.)
        let userId = JSON.parse(AuthService.getUserData()).id;
        let groupId = JSON.parse(this.props.match.params.id);
        let pageSize = 1;
        let pageNr = 1;
        PlanService.GetSimilarPlansPage(pageNr, pageSize, this.state.startTime, this.state.endTime, userId, groupId, this.state.activityDescription).then(
            (plans) => {
                this.setState({
                    similarPlans: true
                })
            },
            (error) => {
                this.setState({
                    similarPlans: false
                })
            }
        );
    }

    setSelectedActivity(key: number, activityDescription: string) {
        this.setState({
            selectedActivity: key,
            activityDescription: activityDescription,
            formValid: this.state.startTime < this.state.endTime
        })
    }

    handleStartTimeInput(e: any) {
        this.setState({
            startTime: e.target.value,
            formValid: this.state.selectedActivity !== Infinity && !!e.target.value && e.target.value < this.state.endTime
        });
    }

    handleEndTimeInput(e: any) {
        this.setState({
            endTime: e.target.value,
            formValid: this.state.selectedActivity !== Infinity && !!e.target.value && e.target.value > this.state.startTime
        });
    }

    back() {
        this.setState({
            homeRedirect: true
        })
    }

    addPlan(e: any) {
        e.preventDefault();
        this.setSimilarPlans();
        this.setState({
            inputData: false
        })
    }

    createNewPlan(){
        alert("caroce aici creez planul");
    }

    getDataFromMap(address: any) {
        this.setState({ address: address })
    }

    homeRedirect(){
        this.setState({homeRedirect: true})
    }

    render() {
        if(this.state.homeRedirect){
            let redirectTo = '/home/';
            return <Redirect to={redirectTo} />;
        }

        return (
            <div>
                <Header />
                {this.state.inputData ?
                    <div>
                        <form className="demoForm" onSubmit={this.addPlan}>
                            {/* {this.state.selectedActivity} */}
                            <div className="form-group">
                                <label> Start Time: </label>
                                <input type="datetime-local" className="form-control" value={this.state.startTime}
                                    onChange={(event) => this.handleStartTimeInput(event)}
                                />
                            </div >
                            <div className="form-group">
                                <label> End Time: </label>
                                <input type="datetime-local" className="form-control" value={this.state.endTime}
                                    onChange={(event) => this.handleEndTimeInput(event)}
                                />
                            </div >
                            <p> {this.state.errorMessage} </p>
                            <button type="submit" className="btn btn-primary"
                                disabled={!this.state.formValid}>Add plan</button>
                            <h2> Select a location </h2>
                            <p> {this.state.address.location} </p>
                            <AddressSelector getDataFromMap={this.getDataFromMap} />
                            <h2> Select an activity: </h2>
                        </form>
                        <SelectActivity setSelectedActivity={this.setSelectedActivity} selectedActivity={this.state.selectedActivity} />
                        <PlanPanel />
                    </div>
                    :
                    <div>
                        {this.state.similarPlans
                            ?
                            <div>
                                 <h3> There are some similar plans to yours. You can join them or you can create a new plan </h3>
                                    {/* // PlanService.GetSimilarPlansPage(1, 6, this.props.startTime, this.props.endTime, this.props.userId, this.props.groupId, this.props.activity).then( */}

                            <SimilarPlansList 
                                startTime={this.state.startTime} 
                                endTime={this.state.endTime} 
                                userId={JSON.parse(AuthService.getUserData()).id}
                                groupId={this.props.match.params.id}
                                activity={this.state.activityDescription} 

                            />
                            </div>
                            :
                            <div>
                                <h3> We could not find any similar plans to yours, a new plan was created.  </h3>

                                <NewPlan 
                                    activity = {this.state.activityDescription}
                                    groupId = {this.props.match.params.id}
                                    startTime = {this.state.startTime}
                                    endTime = {this.state.endTime}
                                    address = {this.state.address}
                                />
                                <button type="button" className="btn btn-warning" onClick={this.homeRedirect}> Back </button>                                                        
                                {/* <Link className="btn btn-warning" to={'/group/' + this.props.match.params.id}/> */}
                            </div>
                        }
                        ...
                    </div>
                }

            </div>
        )
    }
}