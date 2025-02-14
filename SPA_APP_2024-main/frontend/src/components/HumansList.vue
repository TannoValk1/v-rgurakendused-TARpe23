<template>
    <div class="about">
        <h1>Humanslist</h1>
        <div class="humanlist">
            <DataTable :value="humans" v-if="humans.length > 0 ">
                <Column field="id" header="Sündmuse id" style="color: black; " />
                <Column field="Name" header="Nimi" style="color: black;" />
                <Column field="Age" header="Vanus" style="color: black;" />
            </DataTable>
            <div v-else>Inimesed puuduvad</div>
        </div>
    </div>
</template>


<script setup lang="ts">
    import { type Humans } from '@/models/humans';
    import { useHumansStore } from "@/stores/humansStore";
    import { storeToRefs } from "pinia";
    import { defineProps, onMounted, watch, ref } from "vue";
    import { useRoute } from "vue-router";

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
</script>
<style>
</style>