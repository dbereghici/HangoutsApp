import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import { PlanService } from '../../services/PlanService';
import PlanPanel from './PlanPanel';
import AuthService from '../../services/AuthService';

export default class AllPlansOfGroupList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.refresh = this.refresh.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            plansData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                plans: []
            }
        }
    }

    componentDidMount() {
        PlanService.GetAllPlansPage(JSON.parse(AuthService.getUserData()).id, 71, 1, 6).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    refresh(id: number){
        PlanService.GetAllPlansPage(JSON.parse(AuthService.getUserData()).id, 71, 1, 6).then(
            (plansData) => {
                this.setState({
                    plansData: plansData,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    render() {
        let plans = this.state.plansData.plans;
        let plans1 = [];
        let plans2 = [];
        let plans3 = [];

        for (var i = 0; i < this.state.plansData.plans.length; i++) {
            if ((i % 3) == 0)
                plans1.push(plans[i]);
            else if ((i % 3) == 1)
                plans2.push(plans[i]);
            else
                plans3.push(plans[i]);
        }

        return (
            <div>
                <p> {this.state.errorMessage} </p>
                {(this.state.plansData.plans.length === 0) ?
                    <p>
                        {/* There are no friends  */}
                    </p>
                    :
                    <div className="row">
                        <div className="col-sm-4">
                            {
                                plans1.map((plan: any, i: number) =>
                                    <PlanPanel plan={plan} refresh={this.refresh} hideButton={false}/>
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                plans2.map((plan: any, i: number) =>
                                    <PlanPanel plan={plan} refresh={this.refresh} hideButton={false}/>
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                plans3.map((plan: any, i: number) =>
                                    <PlanPanel plan={plan} refresh={this.refresh} hideButton={false}/>
                                )}
                        </div>
                    </div>
                }
            </div>
        )
    }
}