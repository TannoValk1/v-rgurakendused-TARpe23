import { type Humans} from "@/models/humans";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "@/models/api";

export const useHumansStore = defineStore('humansStore', () => {
    const apiGetHumans = useApi<Humans[]>('humans');
    const humans = ref<Humans[]>([]);
    let allHumans: Humans[] = [];

    const loadHumans = async () => {
        await apiGetHumans.request();

        if (apiGetHumans.response.value) {
            return apiGetHumans.response.value;
        }
        return [];
    };

    const load = async () => {
        allHumans = await loadHumans();
        humans.value = allHumans;
    };
    const getHumansById = (id: Number) => {
        return allHumans.find((humans) => humans.id === id);
    };


    const addHumans = async (humans: Humans) => {
        const apiAddHumans = useApi<Humans>('humans', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(humans),
        });

        await apiAddHumans.request();
        if (apiAddHumans.response.value) {
            load();
        }
    };
    const updateHumans = async (humans: Humans) => {
        const apiUpdateHumans = useApi<Humans>('humans/' + humans.id, {
            method: 'PUT',
            headers: { 
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(humans),
        });

        await apiUpdateHumans.request();
        if (apiUpdateHumans.response.value) {
            load();
        }
    };


    const deleteHumans = async (humans: Humans) => {
        const deleteHumansRequest = useApiRawRequest(`humans/${humans.id}`, {
            method: 'DELETE',
        });

        const res = await deleteHumansRequest();

        if (res.status === 204) {
            let id = humans.value.indexOf(humans);

            if (id !== -1) {
                humans.value.splice(id, 1);
            }

            id = humans.value.indexOf(humans);

            if (id !== -1) {
                humans.value.splice(id, 1);
            }
        }
    };

    return { humans, load, getHumansById, addHumans, updateHumans, deleteHumans };
});