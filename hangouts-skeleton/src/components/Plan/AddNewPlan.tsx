import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import SelectActivity from './SelectActivity';
import { Header } from '../Header/Header';
import { AddressSelector } from '../AddressSelector/AddressSelector';
import PlanPanel from './PlanPanel';

export default class AddNewPlan extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.setSelectedActivity = this.setSelectedActivity.bind(this);
        this.back = this.back.bind(this);
        this.addPlan = this.addPlan.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);

        this.state = {
            selectedActivity: Infinity,
            formValid: false,
            startTime: new Date(),
            endTime: new Date(),
            inputData: true,
            address: {
                location: '',
                latitude: 0,
                longitude: 0
            },
        }
    }

    setSelectedActivity(key: number) {
        this.setState({
            selectedActivity: key,
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
            inputData: true
        })
    }

    addPlan(e: any) {
        e.preventDefault();
        this.setState({
            inputData: false
        })
    }

    getDataFromMap(address : any){
        this.setState({address: address})
        // let user = {
        //     email: this.state.email,
        //     password: this.state.password,
        //     firstname: this.state.firstname,
        //     lastname: this.state.lastname,
        //     birthdate: this.state.birthdate,
        //     address: this.state.address
        // }
        // console.log(user)
    }

    render() {

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
                            <button type="submit" className="btn btn-primary"
                                disabled={!this.state.formValid}>Add plan</button>     
                               <h2> Select a location </h2>
                                <p> {this.state.address.location} </p>
                                <AddressSelector getDataFromMap={this.getDataFromMap}/>
                            <h2> Select an activity: </h2>
                        </form>
                        <SelectActivity setSelectedActivity={this.setSelectedActivity} selectedActivity={this.state.selectedActivity} />
                        <PlanPanel />
                    </div>
                    :
                    <div>
                        <h2> Similar plans </h2>
                        ...
                        <button type="button" className="btn btn-warning" onClick={this.back}> Back </button>
                        <button type="button" className="btn btn-warning"> Create new plan </button>
                    </div>
            }

            </div>
        )
    }
}