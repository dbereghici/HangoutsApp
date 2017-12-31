import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';

const logo = require('../../images/plan.png');


export default class PlanPanel extends BaseComponent{
    constructor(props: any){
        super(props);

    }

    render(){
        return(
            <div className="panel panel-danger" style={{width: "200px"}} >
            <div className="panel-heading">
                <img src={logo} width="50px" height="50px" />

                {/* <b>{this.state.UserData.username}</b> */}
            </div>
            <div className="panel-body">
                Plan Caroce
                }
            </div>
        </div>
        )
    }
}