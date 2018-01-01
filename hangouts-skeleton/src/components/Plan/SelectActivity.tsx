import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';

import { activitiesList } from '../Activities';

export default class SelectActivity extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.onClickHandler = this.onClickHandler.bind(this);
        this.renderActivities = this.renderActivities.bind(this);

        this.state = {
            activities: activitiesList,
            selectedActivity: this.props.selectedActivity
        }
    }

    onClickHandler(key: number, activity: string) {
        this.setState({
            selectedActivity: key
        })
        this.props.setSelectedActivity(key, activity);
    }


    renderActivities(activity: any, index: number): JSX.Element {
        let defaultStyle = { backgroundColor: "#177c4f" };
        let onselectStyle = { backgroundColor: "#6bf9c5" }
        return (
            <div className="container">
                <div className="row">
                    <div onClick={() => this.onClickHandler(index, activity)}>
                        <div className="col-md-2">
                            <div className="thumbnail" style={index === this.state.selectedActivity ? defaultStyle : onselectStyle}>
                                <img src={require('../../images/' + activity + '.png')}
                                    width="100px"
                                    height="100px"
                                    alt="Cinque Terre"
                                />
                                <h3> {activity} </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    render() {
        //const logo = require('../../images/group.png');
        //                    <img src={logo} width="50px" height="50px" />

        let activities1 = [];
        let activities2 = [];
        let activities3 = [];
        let activities4 = [];


        for (var i = 0; i < this.state.activities.length; i++) {
            if ((i % 4) == 0)
                activities1.push(this.state.activities[i]);
            else if ((i % 4) == 1)
                activities2.push(this.state.activities[i]);
            else if ((i % 4) == 2)
                activities3.push(this.state.activities[i]);
            else
                activities4.push(this.state.activities[i]);
        }

        return (
            <div>
                <div className="container">
                    <div className="col-sm-2" />
                    <div className="col-sm-2">
                        {activities1.map((activity: any, index: number) => this.renderActivities(activity, index))}
                    </div>
                    <div className="col-sm-2">
                        {activities2.map((activity: any, index: number) => this.renderActivities(activity, index + this.state.activities.length / 4))}
                    </div>
                    <div className="col-sm-2">
                        {activities3.map((activity: any, index: number) => this.renderActivities(activity, index + this.state.activities.length / 4 * 2 ))}
                    </div>
                    <div className="col-sm-2">
                        {activities4.map((activity: any, index: number) => this.renderActivities(activity, index + this.state.activities.length / 4 * 3))}
                    </div>
                    <div className="col-sm-2" />
                </div>
            </div>
        )
    }
}