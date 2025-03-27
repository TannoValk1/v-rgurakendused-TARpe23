<template>
    <EventList :title="title" />
    <PersonList :title="title" />
</template>

<script setup lang="ts">
    import EventList from "@/components/EventList.vue"
    import PersonList from "@/components/HumansList.vue"
    import { defineProps } from "vue";

    defineProps<{ title: String }>();

</script>