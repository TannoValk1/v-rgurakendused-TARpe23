<template>
    <div class="about">
        <h1>Humans List</h1>

        <form @submit.prevent="addHuman">
            <input v-model="newHuman.name" placeholder="Nimi" required />
            <input v-model.number="newHuman.age" type="number" placeholder="Vanus" required />
            <button type="submit">Lisa inimene</button>
        </form>

        <div class="humanlist">
            <DataTable :value="humans">
                <Column field="id" header="ID" />
                <Column field="name" header="Name" />
                <Column field="age" header="Age" />
                <Column header="Actions">
                    <template #body="{ data }">
                        <button @click="deleteHuman(data.id)">Kustuta</button>
                        <button @click="editHuman(data)">Muuda</button>
                    </template>
                </Column>
            </DataTable>
        </div>

        <div v-if="editingHuman" class="edit-form">
            <h2>Muuda Inimest</h2>
            <form @submit.prevent="updateHuman">
                <input v-model="editingHuman.name" placeholder="Nimi" required />
                <input v-model.number="editingHuman.age" type="number" placeholder="Vanus" required />
                <button type="submit">Salvesta</button>
                <button type="button" @click="editingHuman = null">Tühista</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
    import { ref, watch, onMounted } from 'vue';
    import DataTable from 'primevue/datatable';
    import Column from 'primevue/column';
    import { useRoute } from "vue-router";
    import { storeToRefs } from "pinia";
    import { useHumansStore } from "@/stores/humansStore";

    interface Human {
        id: number;
        name: string;
        age: number;
    }

    const route = useRoute();

    watch(route, (to, from) => {
        if (to.path !== from.path || to.query !== from.query) {
            humansStore.load();
        }
    }, { deep: true });

    defineProps<{ title: String }>();
    const humansStore = useHumansStore();
    const { humans } = storeToRefs(humansStore);

    onMounted(async () => {
        humansStore.load();
    });

    const newHuman = ref<Omit<Human, 'id'>>({
        name: "",
        age: null
    });

    const editingHuman = ref<Human | null>(null);

    const addHuman = () => {
        humans.value.push({
            id: humans.value.length ? Math.max(...humans.value.map(h => h.id)) + 1 : 1,
            ...newHuman.value
        });
        newHuman.value = { name: "", age: null };
    };

    const deleteHuman = (id: number) => {
        humans.value = humans.value.filter(p => p.id !== id);
    };

    const editHuman = (human: Human) => {
        editingHuman.value = { ...human };
    };

    const updateHuman = () => {
        if (!editingHuman.value) return;
        const index = humans.value.findIndex(p => p.id === editingHuman.value!.id);
        if (index !== -1) {
            humans.value[index] = { ...editingHuman.value };
        }
        editingHuman.value = null;
    };
</script>
