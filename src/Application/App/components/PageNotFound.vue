<template>
    <div class="d-flex" id="error-page">
        <div class="error-content">
            <h1>404</h1>
            <h2>Oops! This page could not be found</h2>
        </div>
    </div>
</template>

<script>
    export default {
    }
</script>

<style>
    #error-page {
        min-height: 400px;
        color: #fff;
    }

    #error-page .error-content {
        margin: auto;
        max-width: 380px;
        padding: 20px;
    }
    #error-page .error-content h1 {
         font-size: 90px;
         font-weight: bold;
         color: #44b684;
         -webkit-text-shadow: 1px 2px 5px rgba(0, 0, 0, 0.26);
         text-shadow: 1px 2px 5px rgba(0, 0, 0, 0.26);
     }
    #error-page .error-content h2 {
         margin-top: 10px;
         font-size: 30px;
         font-weight: bold;
     }
    #error-page .error-content p {
         margin-top: 20px;
         font-size: 18px;
         line-height: 22px;
     }
</style>
