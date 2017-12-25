import * as React from 'react';
import './AddressSelector.css';
import GoogleMapReact from 'google-map-react';
import { AddressService } from './../../services/AddressService';
// import GoogleMapsLoader from 'react-google-maps-loader';

export class AddressSelector extends React.Component<any, any> {

    mapElement: any;
    marker: any;
    mapsElement: any;

    constructor(props: any) {
        super(props);

        this.state = {
            fullAddress: null,
            defaultCenter: { lat: 46.2017559, lng: 6.1466014 }
        };
    }

    setInitialAddress() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position) => {
                let location = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                }
                if (this.mapElement && this.mapsElement) {
                    this.renderMarker(this.mapElement, this.mapsElement, location)
                }
                this.setState({ defaultCenter: location });
                this.getAddressFromCoord(this.state.defaultCenter);
            });
        }
    }

    getAddressFromCoord(coord: any) {
        AddressService.getAddressFromCoords(coord).then(
            (address) => {
                if (address) 
                    this.setState({ fullAddress: address.formatted_address });
                else
                    this.setState({ fullAddress: "No address found!" });
                this.props.getDataFromMap({latitude : coord.lat, longitude : coord.lng, location : address ? address.formatted_address : ''})
            },
            (error) => {

            }
        )
    }

    onMapClick = (value: any) => {
        let coords = { lat: value.lat, lng: value.lng };
        this.renderMarker(this.mapElement, this.mapsElement, coords);
        this.getAddressFromCoord(coords);
    }

    onGoogleMapsLoad(map: any, maps: any) {
        // let defaultPosition = {
        //     lat : 59.01,
        //     lng : 30.04
        // }
        this.mapElement = map;
        this.mapsElement = maps;
        this.setInitialAddress();
        this.getAddressFromCoord({ lat: this.state.defaultCenter.lat, lng: this.state.defaultCenter.lng });

        let address = {
            locatie : this.state.fullAddress,
            latitudine : this.state.defaultCenter.lat,
            longitudine : this.state.defaultCenter.lng
        }
        this.props.getDataFromMap(address);
        // this.renderMarker(map, maps,  defaultPosition)
    }

    renderMarker(map: any, maps: any, position: any) {
        if (this.marker) {
            this.marker.setPosition(position);
        } else {
            this.marker = new maps.Marker({
                position,
                map,
                title: 'My address!'
            });
        }
        return this.marker;
    }

    render() {
        let key = "AIzaSyCRXBEiZM7ElvboRvqjQNFJC3EMPTrO5lI";
        return (
            <div className="container">

                {/* <div className="row">
                    <div className="col-sm-12">

                        <form className="form-inline">
                            <div className="row">
                                <div className="col-xs-8 col-sm-10">

                                    <div className="form-group">
                                        <label className="sr-only" htmlFor="address">Address</label>
                                        <input type="text"
                                            className="form-control input-lg"
                                            id="address"
                                            placeholder="London"
                                            required />
                                    </div>

                                </div>

                                <div className="col-xs-4 col-sm-2">

                                    <button type="submit" className="btn btn-default btn-lg">
                                        <span className="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </button>

                                </div>
                            </div>
                        </form>

                    </div>
                </div> */}
                <div className="row">
                    <div className="col-sm-12">
                        <p className="bg-info"> {this.state.fullAddress} </p>
                        <div className="map">
                            <GoogleMapReact
                                bootstrapURLKeys={{ key }}
                                center={this.state.defaultCenter}
                                zoom={9}
                                onClick={this.onMapClick}
                                onGoogleApiLoaded={({ map, maps }) => this.onGoogleMapsLoad(map, maps)}
                            />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
