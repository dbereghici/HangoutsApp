import * as React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';

import './App.css';
import 'font-awesome/css/font-awesome.css';

// import { Header } from './../src/components/Header/Header';
// import { Body } from './../src/components/Body/Body';
import Home from './components/Home/Home'
import { Route, Switch } from 'react-router';
import { Authentication } from './components/Authentication/Authentication';
import { MyProfile } from './components/MyProfile/MyProfile';
import { Friends } from './components/Friends/Friends';
import { Groups } from './components/Group/Groups';
import Chat from './components/Chat/Chat';
import { Group } from './components/Group/Group';
import { UserPage } from './components/User/UserPage';
import AddNewPlan from './components/Plan/AddNewPlan';
import { MembersList } from './components/Plan/MembersList';
import NotFoundComponent from './components/NotFoundComponent';
// import AdministrateGroup from './components/Group/AdministrateGroup';


class App extends React.Component {
  render() {
    return (
      <Router>
        <div className="app">
          {/* <Header />
          <Body /> */}
          <Switch>
            <Route path="/" exact={true} component={Authentication} />
            <Route path="/home" component={Home} />
            <Route path="/authentication" component={Authentication} />
            <Route path="/myprofile" component={MyProfile} />
            <Route path="/friends" component={Friends} />
            <Route path="/groups" component={Groups} />
            <Route path="/chat/:type/:id/" component={Chat} />
            <Route path="/group/:id" component={Group} />
            <Route path="/users" component={UserPage} />
            <Route path="/plan/group/:id" component={AddNewPlan} />
            <Route path="/members/plan/:id" component={MembersList} />
            <Route path="*" exact={true} component={NotFoundComponent} />
            {/* <Route path="/group/:id/administrate" component={AdministrateGroup} /> */}
          </Switch>
        </div>
      </Router>
    );
  }
}

export default App;
