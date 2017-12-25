import axios from 'axios';

export class AddressService {
    private static root: string = 'http://maps.googleapis.com/maps/api/geocode/json';


    public static getAddressFromCoords(coord: any): Promise<any> {
        return new Promise((resolve, reject) => {
            axios.get(this.root, {
                params : {
                    latlng : `${coord.lat},${coord.lng}`
                }
            }).then((response: any) => {
                let result = response.data.results;
                if (result && result.length){
                    let address = result[0];
                    resolve(address);
                } else{
                    resolve(null);
                }
            },
                (error: any) => {
                    reject(error);
                })
        });
    }
}